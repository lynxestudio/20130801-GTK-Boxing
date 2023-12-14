using System;

namespace TestBoxing
{
	public struct MovementItem
	{
		DateTime _date;
		char _sign;
		double _amount;
		public DateTime Date {
			set{ _date = value;}
			get{ return _date;}
		}
		public char Sign {
			set{ _sign = value;}
			get{ return _sign;}
		}
		public double Amount {
			set{ _amount = value;}
			get{ return _amount;}
		} 
		public MovementItem (DateTime date, 
		                     char sign, 
		                     double amount)
		{
			this._date = date;
			this._sign = sign;
			this._amount = amount;
		}
	}
}