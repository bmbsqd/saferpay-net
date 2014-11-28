using System.Globalization;
using System.Linq;

namespace Bmbsqd.SaferPay
{
	public struct CreditCardExpiration
	{
		private int _year;
		private int _month;

		public CreditCardExpiration( int year, int month )
		{
			_year = year % 100;
			_month = month;
		}

		public int Year
		{
			get { return _year; }
			set { _year = value%100; }
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
			var m = text.Substring( 0, 2 );
			var y = text.Substring( 2 );
			return new CreditCardExpiration {
				_month = int.Parse( m ),
				_year = int.Parse( y ) % 100
			};
		}
	}
}