using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnsekGlobal.Models
{
	public class MeterReadingsDM
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Int64 MeterReadingId { get; set; }

		[ForeignKey("AccountId")]
		public Int64 AccountId { get; set; }
		public AccountDM Account { get; set; }

		[TypeConverter(typeof(CustomerDateTimeConverter))]
		public DateTime MeterReadingDateTime { get; set; }
		[TypeConverter(typeof(CustomInt64Converter))]
		[DisplayFormat(DataFormatString = "{0:D5}")]
		public Int64? MeterReadValue { get; set; }

		[NotMapped]
		public bool IsError { get; set; }
		[NotMapped]
		public string Message { get; set; }
	}

	public sealed class MeterReadingsDMMap : ClassMap<MeterReadingsDM>
    {
		public MeterReadingsDMMap()
        {
			AutoMap(System.Globalization.CultureInfo.InvariantCulture);
			Map(m => m.MeterReadingId).Ignore();
			Map(m => m.Account).Ignore();
			Map(m => m.Account.AccountId).Ignore();
			Map(m => m.Account.FirstName).Ignore();
			Map(m => m.Account.LastName).Ignore();
			Map(m => m.Account.FullName).Ignore();
			Map(m => m.IsError).Ignore();
			Map(m => m.Message).Ignore();
		}
	}

	public class CustomerDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
			return DateTime.Parse(text);
        }
    }

	public class CustomInt64Converter : Int64Converter
	{
		public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
		{
			Int64 num;
			if (!Int64.TryParse(text, out num))
				return null;
			return base.ConvertFromString(text, row, memberMapData);
		}
	}
}
