using EnsekGlobal.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnsekGlobal
{
	public class CSVHelper
	{
        public static async Task<string> GenerateCSVJson(IFormFile csv)
        {
            using (var ms = new MemoryStream())
            {
                csv.CopyTo(ms);
                var bytes = ms.ToArray();
                var base64 = Convert.ToBase64String(bytes);

                var base64Model = new CsvModel { Base64 = base64 };
                return JsonSerializer.Serialize(base64Model);
            }
        }
	}
}
