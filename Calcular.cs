/*
 * Usuario: GNR092
 * Fecha: 02/02/2016
 * Hora: 03:48 p.m.
 * 
 */
using System;
using System.IO;
using System.Security.Cryptography;
	
namespace CreadorDeParches
{
	public static class Calcular
	{
		public static byte[] CalcularMD5(HashAlgorithm MD5hash,Stream Archivo)
		{
			byte[] hashmd5 = MD5hash.ComputeHash(Archivo);
			return MD5hash.ComputeHash(hashmd5);
		}
	}
}
