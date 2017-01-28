﻿using System;
using System.Net.Sockets;

namespace SharpModbus
{
	public static class TcpTools
	{
		public static TcpClient ConnectWithTimeout(string host, int port, int timeout)
		{
			var socket = new TcpClient();
			var result = socket.BeginConnect(host, port, null, null);
			if (!result.AsyncWaitHandle.WaitOne(timeout, true))
				Thrower.Throw("Timeout connecting to {0}:{1}", host, port);
			socket.EndConnect(result);
			return socket;
		}
	}
	
	static class Assert
	{
		
		public static void Equal(int a, int b, string format)
		{
			if (a != b)
				throw new Exception(string.Format(format, a, b));
		}
		
		public static void Equal(ushort a, ushort b, string format)
		{
			if (a != b)
				throw new Exception(string.Format(format, a, b));
		}
		
		public static void Equal(byte a, byte b, string format)
		{
			if (a != b)
				throw new Exception(string.Format(format, a, b));
		}
		
		public static void Equal(byte a, byte b, int i, string format)
		{
			if (a != b)
				throw new Exception(string.Format(format, a, b, i));
		}
	}
	
	static class Closer
	{
		//SerialPort, Socket, TcpClient, Streams, Writers, Readers, ...
		public static void Close(IDisposable closeable)
		{
			try {
				if (closeable != null)
					closeable.Dispose();
			} catch (Exception) {
			}
		}

		public static void Close(TcpListener closeable)
		{
			try {
				if (closeable != null)
					closeable.Stop();
			} catch (Exception) {
			}
		}

	}
		
	static class Thrower
	{
		public static void Throw(string format, params object[] args)
		{
			var message = format;
			if (args.Length > 0) {
				message = string.Format(format, args);
			}
			throw new Exception(message);
		}
		
		public static void Throw(Exception inner, string format, params object[] args)
		{
			var message = format;
			if (args.Length > 0) {
				message = string.Format(format, args);
			}
			throw new Exception(message, inner);
		}
	}
}
