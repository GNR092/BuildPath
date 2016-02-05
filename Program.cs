/*
 * Usuario: GNR092
 * Fecha: 04/02/2016
 * Hora: 11:09 p.m.
 * 
 */
using System;
using System.Windows.Forms;

namespace CreadorDeParches
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new BuildPath());
		}
		
	}
}
