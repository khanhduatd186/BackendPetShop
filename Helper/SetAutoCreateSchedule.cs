using ApiPetShop.Data;
using ApiPetShop.Models;
using AutoMapper;
using Hangfire;

namespace ApiPetShop.Helper
{
    public class SetAutoCreateSchedule
    {
       

        
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRecurringJobManager _recurringJobManager;

        public SetAutoCreateSchedule(ApplicationDbContext context, IMapper mapper, IRecurringJobManager recurringJobManager)
        {
            _context = context;
            _mapper = mapper;
            _recurringJobManager = recurringJobManager;

        }
        //public void InitializeJobs()
        //{
        //    // Lên lịch công việc xóa lịch cũ và thêm lịch mới mỗi ngày lúc 00:00
        //    _recurringJobManager.AddOrUpdate("daily-schedule-job", () => UpdateAndDelete(), Cron.Daily);
        //}
        public void UpdateAndDelete()
        {
            DateTime currentDate = DateTime.Today;
            var oldSchedules = _context.Service_Details.Where(l=>l.NgayThucHien.Date < currentDate).ToList();
            _context.Service_Details.RemoveRange(oldSchedules);

            var times = _context.Times.ToList();
            var services = _context.services.ToList();
            foreach ( var service in services )
            {
                foreach (var time in times)
                {
                    var newSchedule = new Service_Detail
                    {
                        NgayThucHien = currentDate,
                        IdTime = time.Id,
                        IdService = service.Id,
                        SoLuongCa = 3
                    };
                    _context.Service_Details.Add(newSchedule);
                }
            }
            _context.SaveChanges();
        }
        private bool IsTwoMonthsPast(DateTimeOffset targetDate)
        {
            // Lấy ngày tháng năm hiện tại
            DateTime currentDate = DateTime.Now;

            // Tính toán sự chênh lệch giữa ngày tháng năm hiện tại và ngày tháng năm đích
            TimeSpan delta = currentDate - targetDate;

            // Kiểm tra xem chênh lệch có phải là 2 tháng không
            if (delta.Days >= 60)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CheckBaoHanh()
        {
            var bills = _context.Bills.ToList();
            foreach( var b in bills )
            {
                if (IsTwoMonthsPast(b.dateTime))
                {
                    var Bill = new BillModel

                    {
                        Id = b.Id,
                        dateTime = b.dateTime,
                        IdUser = b.IdUser,
                        Price = b.Price,
                        Status = b.Status,
                        IsDelete = 1


                    };
                    var updateBill = _mapper.Map<Bill>(Bill);
                    _context.Bills.Update(updateBill);
                }
                _context.SaveChanges(true);
            }

        }

    }
}
