#if NET40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace System.Net
{
	internal static class IpAddressExtensions
	{
		public static IPAddress MapToIPv6(this IPAddress iPAddress)
		{
			if (iPAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				return iPAddress;
			}

#pragma warning disable CS0618 // Тип или член устарел
			var ushorts = new ushort[8]
						{
				0,
				0,
				0,
				0,
				0,
				65535,
				(ushort)(((iPAddress.Address & 0xFF00) >> 8) | ((iPAddress.Address & 0xFF) << 8)),
				(ushort)(((iPAddress.Address & 0xFF000000u) >> 24) | ((iPAddress.Address & 0xFF0000) >> 8))
						};
#pragma warning restore CS0618 // Тип или член устарел
			byte[] bytes=new byte[ushorts.Length*2];
			Buffer.BlockCopy(ushorts, 0, bytes, 0, ushorts.Length*2);
			return new IPAddress(bytes, 0u);
		}
	}
}
#endif
