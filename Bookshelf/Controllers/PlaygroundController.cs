using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    public class PlaygroundController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> PowerOfTwo(string number)
        {

            double inputValue;
            if (!double.TryParse(number, out inputValue))
            {
                ViewData["PowerOfTwo_Error"] = "Sorry! Not a valid number!";
            }
            else {
                var power = FindPowerOfTwo(inputValue);                
                if (!power.HasValue)
                {
                    ViewData["PowerOfTwo_Error"] = $"{inputValue} is not a 2-power!";
                }
                else
                {
                    ViewData["PowerOfTwo_Result"] = $"{inputValue} is a 2-power! The power is: {power}";
                }
            }

            return View(nameof(Index));
        }

        private int? FindPowerOfTwo(double number)
        {
            const int maxPower = 1024;

            var power = 0;
            var found = false;
            while (power < maxPower && !found)
            {
                var result = Math.Pow(2, power);
                found = result == number;

                if (!found)
                {
                    power++;
                }
            }
            return found ? power : default(int?);
        }

        public async Task<IActionResult> Reverse(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var reversedValue = string.Join("", value.Reverse());
                ViewData["Reverse_Result"] = reversedValue;
            }

            return View(nameof(Index));
        }

        public async Task<IActionResult> Replicate(string value, int noOfTimes)
        {
            if (!string.IsNullOrEmpty(value) && noOfTimes > 0)
            {
                var result = string.Join("", Enumerable.Repeat(value, noOfTimes));
                ViewData["Replicate_Result"] = result;
            }

            return View(nameof(Index));
        }

        public async Task<IActionResult> PrintOddNumbers()
        {
            var allNumbers = Enumerable.Range(0, 100);
            var oddNumbers = allNumbers.Where(n => n % 2 != 0);
            
            ViewData["PrintOddNumbers_Result"] = string.Join(" " , oddNumbers);
            return View(nameof(Index));
        }
    }
}