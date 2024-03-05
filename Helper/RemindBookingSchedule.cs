using ApiPetShop.Data;
using Application.Interfaces;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Helper
{
    public class RemindBookingSchedule
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public RemindBookingSchedule(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task SendMailRemind()
        {

            var currentDate = DateTime.Today;
            var nextDay = currentDate.AddDays(1);
            var endOfDay = nextDay.AddDays(1).AddTicks(-1); // Kết thúc ngày (23:59:59)

            var serviceCartsForReminder = _context.service_Carts
                .Where(cart => cart.dateTime >= nextDay && cart.dateTime <= endOfDay)
                .ToList();

            foreach (var item in serviceCartsForReminder)
            {
                var user = _context.Users.First(s => s.Id == item.IdUser);
                await _emailService.SendMailRemind(
                    user.Email, 
                    user.Name,
                    item.Time + " " + item.dateTime.ToString().Split(" ").First().Replace("T00:00:00", ""));
            }
        }
        public static char LayKyTuDauTien(string input)
        {
            // Kiểm tra xem chuỗi có rỗng không
            if (!string.IsNullOrEmpty(input))
            {
                // Lấy ký tự đầu tiên
                char kyTuDauTien = input[0];
                return kyTuDauTien;
            }
            else
            {
                // Trả về một giá trị mặc định nếu chuỗi rỗng
                return '\0'; // Trả về ký tự null
            }
        }
        static string GetCharactersBeforeH(string input)
        {
            // Tìm vị trí của chữ 'h' đầu tiên trong chuỗi
            int indexOfFirstH = input.IndexOf('h');

            // Kiểm tra nếu chữ 'h' tồn tại trong chuỗi
            if (indexOfFirstH != -1)
            {
                // Lấy chuỗi ký tự trước chữ 'h'
                string charactersBeforeH = input.Substring(0, indexOfFirstH);
                return charactersBeforeH;
            }

            // Trả về null nếu không tìm thấy chữ 'h'
            return null;
        }
       
        public async Task SendMailRemind2()
        {
           
            var currentDate = int.Parse(DateTime.Now.Hour.ToString());
            var nowDay = DateTime.Now.ToString().Split(" ").First().Replace("T00:00:00", "");

            var gio = GetCharactersBeforeH("15h-17h");
            var serviceCartsForReminder = _context.service_Carts.ToList();

            List<Service_Cart> services = new List<Service_Cart>();
            foreach (var service in serviceCartsForReminder)
            {
                if((int.Parse(GetCharactersBeforeH(service.Time)) - currentDate == 1) && nowDay.Equals(service.dateTime.ToString().Split(" ").First().Replace("T00:00:00", "")))
                {
                    services.Add(service);
                }
            }

            foreach (var item in services)
            {
                 var user = _context.Users.First(s => s.Id == item.IdUser);
                 await _emailService.SendMailRemind(
                     user.Email,
                     user.Name,
                     item.Time + " " + item.dateTime.ToString().Split(" ").First().Replace("T00:00:00", ""));
            }

        }
    }
}