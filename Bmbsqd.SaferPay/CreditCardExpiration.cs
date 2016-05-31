using System.Globalization;
using System.Linq;

namespace Bmbsqd.SaferPay
{
	public struct CreditCardExpiration
	{
		private int _year;
		private int _month;

		private static int FixYear( int y ) => y < 2000 ? 2000 + y : y;

		public CreditCardExpiration( int year, int month )
		{
			_year = year % 100;
			_month = month;
		}

		public int Year
		{
			get { return _year; }
			set { _year = FixYear(value); }
		}

		public int Month
		{
			get { return _month; }
			set { _month = value; }
		}

		public override string ToString()
		{
			return
				_month.ToString( CultureInfo.InvariantCulture ).PadLeft( 2, '0' ) +
				_year.ToString( CultureInfo.InvariantCulture ).PadLeft( 2, '0' );
		}

		public static CreditCardExpiration Parse( string text )
		{
			text = new string( text.Where( char.IsNumber ).ToArray() );
			var m = int.Parse(text.Substring( 0, 2 ));
			var y = int.Parse(text.Substring( 2 ));
			
			return new CreditCardExpiration {
				_month = m,
				_year = FixYear(y)
			};
		}
	}
}
