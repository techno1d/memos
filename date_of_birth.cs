/// <summary>  
/// For calculating age  
/// </summary>  
/// <param name="dob">Enter Date of Birth to Calculate the age</param>  
/// <returns> years, months,days, hours...</returns>  
public static string CalculateYourAge(DateTime dob)
{
	DateTime now = DateTime.Now;
	int years = new DateTime(DateTime.Now.Subtract(dob).Ticks).Year - 1;
	DateTime pastYearDate = dob.AddYears(years);
	
	int months = 0;
	for (int i = 1; i <= 12; i++)
	{
		if (pastYearDate.AddMonths(i) == now)
		{
			months = i;
			break;
		}
		else if (pastYearDate.AddMonths(i) >= now)
		{
			months = i - 1;
			break;
		}
	}
	int days = now.Subtract(pastYearDate.AddMonths(months)).Days;
	int hours = now.Subtract(pastYearDate).Hours;
	int minutes = now.Subtract(pastYearDate).Minutes;
	int seconds = now.Subtract(pastYearDate).Seconds;
	
	return string.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)", years, months, days, hours, seconds);
}

/// <summary>  
/// For calculating only age  
/// </summary>  
/// <param name="dateOfBirth">Date of birth</param>  
/// <returns> age e.g. 26</returns>  
public static int CalculateAge(DateTime dateOfBirth)
{
	int age = 0;
	age = DateTime.Now.Year - dateOfBirth.Year;
	if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
		age = age - 1;			
	
	return age;
}

/// <summary>  
/// For calculating only age  
/// </summary>  
/// <param name="dayOfBirth">Day of birth</param>  
/// <param name="monthOfBirth">Month of birth</param>  
/// <param name="yearOfBirth">Year of birth</param>  
/// <returns> age e.g. 26</returns>  
public static int CalculateAge(int dayOfBirth, int monthOfBirth, int yearOfBirth)
{
	DateTime dateOfBirth = new DateTime(yearOfBirth, monthOfBirth, dayOfBirth);
	
	int age = 0;
	age = DateTime.Now.Year - dateOfBirth.Year;
	if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
		age = age - 1;
	
	return age;
}
