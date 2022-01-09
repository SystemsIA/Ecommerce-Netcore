using System.ComponentModel.DataAnnotations;

namespace App.Utils
{
	public static class Currency
	{
		// Valor en soles: 1 Dolar <> 3.94 soles
		[DataType(DataType.Currency)] public static double Dolar = 3.94;
	}
}