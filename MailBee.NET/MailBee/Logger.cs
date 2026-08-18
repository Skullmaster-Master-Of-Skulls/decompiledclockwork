using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using a;
using a.g;

namespace MailBee
{
	// Token: 0x02000072 RID: 114
	public class Logger
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x000091E8 File Offset: 0x000081E8
		internal Logger(bo A_0)
		{
			this.d = A_0;
			this.e = null;
			this.f = null;
			this.h = Global.DefaultEncoding;
			this.g = new object();
			this.k = false;
			this.l = false;
			this.m = false;
			this.i = "log.txt";
			this.j = null;
			this.n = false;
			this.p = true;
			this.q = false;
			this.o = false;
			this.r = 0;
			this.s = LogFormatOptions.None;
			this.v = false;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000929F File Offset: 0x0000829F
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x000092A7 File Offset: 0x000082A7
		public bool Enabled
		{
			get
			{
				return this.l;
			}
			set
			{
				this.l = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x000092B0 File Offset: 0x000082B0
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x000092B8 File Offset: 0x000082B8
		public bool DisableOnException
		{
			get
			{
				return this.m;
			}
			set
			{
				this.m = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000092C1 File Offset: 0x000082C1
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000092C9 File Offset: 0x000082C9
		public Encoding FileEncoding
		{
			get
			{
				return this.h;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.h = value;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000092E0 File Offset: 0x000082E0
		private bool b(bool A_0)
		{
			if (this.l && !this.n && !this.k && this.i != null)
			{
				try
				{
					this.e = new StreamWriter(this.i, A_0, this.h);
					this.e.AutoFlush = true;
					this.k = true;
					return true;
				}
				catch (UnauthorizedAccessException a_)
				{
					if (this.m)
					{
						this.l = false;
						return false;
					}
					throw new MailBeeIOException(32, a_);
				}
				catch (IOException a_2)
				{
					if (this.m)
					{
						this.l = false;
						return false;
					}
					throw new MailBeeIOException(30, a_2);
				}
				return false;
			}
			return false;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00009398 File Offset: 0x00008398
		internal void a()
		{
			if (this.k)
			{
				this.k = false;
				this.e.Close();
				this.e = null;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000093BB File Offset: 0x000083BB
		// (set) Token: 0x060003CB RID: 971 RVA: 0x000093C4 File Offset: 0x000083C4
		public string Filename
		{
			get
			{
				return this.i;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				value = value.Trim();
				if (value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				if (this.i != value)
				{
					this.a();
					this.i = value;
				}
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00009414 File Offset: 0x00008414
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000941C File Offset: 0x0000841C
		public string OldFilename
		{
			get
			{
				return this.j;
			}
			set
			{
				if (value == null)
				{
					this.j = null;
					return;
				}
				if (value.Trim() == string.Empty)
				{
					this.j = null;
					return;
				}
				this.j = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000944A File Offset: 0x0000844A
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00009452 File Offset: 0x00008452
		public bool MemoryLog
		{
			get
			{
				return this.n;
			}
			set
			{
				if (this.n != value)
				{
					this.a();
					this.n = value;
				}
				if (this.n && this.f == null)
				{
					this.f = new StringBuilder(65536);
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000948A File Offset: 0x0000848A
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00009492 File Offset: 0x00008492
		public bool KeepLogFileOpen
		{
			get
			{
				return this.o;
			}
			set
			{
				if (!value)
				{
					this.a();
				}
				this.o = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000094A4 File Offset: 0x000084A4
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x000094AC File Offset: 0x000084AC
		public bool HidePasswords
		{
			get
			{
				return this.p;
			}
			set
			{
				this.p = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000094B5 File Offset: 0x000084B5
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x000094BD File Offset: 0x000084BD
		public bool LogDnsQueryBody
		{
			get
			{
				return this.q;
			}
			set
			{
				this.q = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000094C6 File Offset: 0x000084C6
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x000094CE File Offset: 0x000084CE
		public int MaxSize
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000094D7 File Offset: 0x000084D7
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x000094DF File Offset: 0x000084DF
		public LogFormatOptions Format
		{
			get
			{
				return this.s;
			}
			set
			{
				this.s = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000094E8 File Offset: 0x000084E8
		// (set) Token: 0x060003DB RID: 987 RVA: 0x000094F0 File Offset: 0x000084F0
		public string DateTimeFormatFull
		{
			get
			{
				return this.t;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				this.t = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00009511 File Offset: 0x00008511
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00009519 File Offset: 0x00008519
		public string DateTimeFormatTimeOnly
		{
			get
			{
				return this.u;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				this.u = value;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000953A File Offset: 0x0000853A
		public string GetMemoryLog()
		{
			if (this.f == null)
			{
				return string.Empty;
			}
			return this.f.ToString();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00009558 File Offset: 0x00008558
		public void Clear()
		{
			if (this.l)
			{
				if (this.n)
				{
					this.f.Length = 0;
					return;
				}
				this.a();
				object obj = this.g;
				lock (obj)
				{
					if (this.b(false) && !this.o)
					{
						this.a();
					}
				}
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000095CC File Offset: 0x000085CC
		private string a(LogMessageType A_0)
		{
			switch (A_0)
			{
			case LogMessageType.Info:
				return Resources.Instance.Log_MessageTypeInfo;
			case LogMessageType.Recv:
				return Resources.Instance.Log_MessageTypeRecv;
			case LogMessageType.Send:
				return Resources.Instance.Log_MessageTypeSend;
			default:
				return Resources.Instance.Log_MessageTypeUser;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00009618 File Offset: 0x00008618
		private string a(bc A_0)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			for (bc bc = A_0; bc != null; bc = bc.bj())
			{
				string text3 = Convert.ToString(bc.bb(), 16);
				while (text3.Length < 2)
				{
					text3 = "0" + text3;
				}
				text = string.Concat(new string[]
				{
					bc.er(),
					"-",
					text3,
					text2,
					text
				});
				text2 = ".";
			}
			if (23 - text.Length > 0)
			{
				text += new string('.', 23 - text.Length);
			}
			return text;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000096B8 File Offset: 0x000086B8
		private string a(string A_0, string A_1, LogMessageType A_2, bc A_3)
		{
			if (!this.q && A_3 is global::a.g.p && (A_2 == LogMessageType.Recv || A_2 == LogMessageType.Send))
			{
				return null;
			}
			if (A_0 == null)
			{
				A_0 = string.Empty;
			}
			if (A_1 == null)
			{
				A_1 = string.Empty;
			}
			else if (A_0 == string.Empty)
			{
				A_1 = "[" + A_1 + "]";
			}
			else
			{
				A_1 = " [" + A_1 + "]";
			}
			string format;
			if ((this.s & LogFormatOptions.AddDate) > LogFormatOptions.None)
			{
				format = this.t;
			}
			else
			{
				format = this.u;
			}
			string text = string.Empty;
			if ((this.s & LogFormatOptions.AddContextInfo) == LogFormatOptions.AddContextInfo)
			{
				text = string.Concat(new string[]
				{
					"[",
					Thread.CurrentThread.GetHashCode().ToString("X4"),
					"] [",
					this.a(A_3),
					"] "
				});
			}
			DateTime now = DateTime.Now;
			string text2 = string.Concat(new string[]
			{
				"[",
				now.ToString(format),
				"] [",
				this.a(A_2),
				"] ",
				text
			});
			if (A_3 != null)
			{
				LogEntry logEntry = new LogEntry(now, A_2, text, text2, A_0, A_1);
				A_3.a(logEntry);
				if (!logEntry.AddThisEntry)
				{
					return null;
				}
				A_0 = logEntry.MessageText;
				A_1 = logEntry.MessageComment;
			}
			return text2 + A_0.Replace("\r\n", "\\r\\n") + A_1;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00009830 File Offset: 0x00008830
		public void WriteLine(string messageText)
		{
			this.b(messageText, null, LogMessageType.User, null);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000983C File Offset: 0x0000883C
		internal void b(string A_0, string A_1, LogMessageType A_2, bc A_3)
		{
			if (this.l)
			{
				string text = this.a(A_0, A_1, A_2, A_3);
				if (text == null)
				{
					return;
				}
				if (!this.v)
				{
					object obj = this.g;
					lock (obj)
					{
						if (!this.v)
						{
							text = this.a(string.Format(Resources.Instance.Log_AssemblyVersion0, "11.2.0 build 590 for .NET 4.5"), null, LogMessageType.Info, A_3) + "\r\n" + text;
							this.v = true;
						}
					}
				}
				if (this.n)
				{
					if (this.r > 0 && this.f.Length + text.Length + 2 > this.r)
					{
						int num = this.f.ToString().IndexOf("\r\n", this.f.Length / 2);
						if (num < 0)
						{
							num = this.f.Length;
						}
						else
						{
							num += 2;
						}
						this.f.Remove(0, num);
					}
					this.f.Append(text + "\r\n");
					return;
				}
				try
				{
					object obj = this.g;
					lock (obj)
					{
						if (this.b(true))
						{
							if (this.r > 0 && this.e.BaseStream.Length + (long)text.Length + 2L > (long)this.r)
							{
								this.a();
								if (this.j != null)
								{
									if (File.Exists(this.j))
									{
										File.Delete(this.j);
									}
									File.Move(this.i, this.j);
								}
								if (!this.b(false))
								{
									return;
								}
							}
							this.e.WriteLine(text);
							if (!this.o)
							{
								this.a();
							}
						}
					}
				}
				catch (IOException a_)
				{
					if (!this.m)
					{
						throw new MailBeeLocalException(30, a_);
					}
					this.l = false;
				}
				catch (UnauthorizedAccessException a_2)
				{
					if (!this.m)
					{
						throw new MailBeeLocalException(32, a_2);
					}
					this.l = false;
				}
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00009A7C File Offset: 0x00008A7C
		internal void a(byte[] A_0, int A_1, int A_2, LogMessageType A_3)
		{
			this.a(A_0, A_1, A_2, A_3, null);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00009A8C File Offset: 0x00008A8C
		internal void a(byte[] A_0, int A_1, int A_2, LogMessageType A_3, bc A_4)
		{
			if ((this.s & LogFormatOptions.BinaryAsText) > LogFormatOptions.None)
			{
				this.b(Global.DefaultEncoding.GetString(A_0, A_1, A_2), null, A_3, A_4);
				return;
			}
			this.b(Convert.ToBase64String(A_0, A_1, A_2), Resources.Instance.Log_Base64Banner, A_3, A_4);
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00009AD9 File Offset: 0x00008AD9
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00009AE1 File Offset: 0x00008AE1
		public object SyncRoot
		{
			get
			{
				return this.g;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.g = value;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00009AF8 File Offset: 0x00008AF8
		private Task<bool> a(bool A_0)
		{
			Logger.d d;
			d.c = this;
			d.d = A_0;
			d.b = AsyncTaskMethodBuilder<bool>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<Logger.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00009B48 File Offset: 0x00008B48
		public Task ClearAsync()
		{
			Logger.b b;
			b.c = this;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<Logger.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00009B8D File Offset: 0x00008B8D
		public Task WriteLineAsync(string messageText)
		{
			return this.c(messageText, null, LogMessageType.User, null);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00009B9C File Offset: 0x00008B9C
		internal Task c(string A_0, string A_1, LogMessageType A_2, bc A_3)
		{
			Logger.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.f = A_2;
			a.g = A_3;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<Logger.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00009C02 File Offset: 0x00008C02
		internal Task b(byte[] A_0, int A_1, int A_2, LogMessageType A_3)
		{
			return this.b(A_0, A_1, A_2, A_3, null);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00009C10 File Offset: 0x00008C10
		internal Task b(byte[] A_0, int A_1, int A_2, LogMessageType A_3, bc A_4)
		{
			if ((this.s & LogFormatOptions.BinaryAsText) > LogFormatOptions.None)
			{
				return this.c(Global.DefaultEncoding.GetString(A_0, A_1, A_2), null, A_3, A_4);
			}
			return this.c(Convert.ToBase64String(A_0, A_1, A_2), Resources.Instance.Log_Base64Banner, A_3, A_4);
		}

		// Token: 0x04000183 RID: 387
		private const int a = 65536;

		// Token: 0x04000184 RID: 388
		private const int b = 23;

		// Token: 0x04000185 RID: 389
		private const string c = "log.txt";

		// Token: 0x04000186 RID: 390
		private bo d;

		// Token: 0x04000187 RID: 391
		private StreamWriter e;

		// Token: 0x04000188 RID: 392
		private StringBuilder f;

		// Token: 0x04000189 RID: 393
		private object g;

		// Token: 0x0400018A RID: 394
		private Encoding h;

		// Token: 0x0400018B RID: 395
		private string i;

		// Token: 0x0400018C RID: 396
		private string j;

		// Token: 0x0400018D RID: 397
		private bool k;

		// Token: 0x0400018E RID: 398
		private bool l;

		// Token: 0x0400018F RID: 399
		private bool m;

		// Token: 0x04000190 RID: 400
		private bool n;

		// Token: 0x04000191 RID: 401
		private bool o;

		// Token: 0x04000192 RID: 402
		private bool p;

		// Token: 0x04000193 RID: 403
		private bool q;

		// Token: 0x04000194 RID: 404
		private int r;

		// Token: 0x04000195 RID: 405
		private LogFormatOptions s;

		// Token: 0x04000196 RID: 406
		private string t = "MM\\/dd\\/yyyy HH:mm:ss.ff";

		// Token: 0x04000197 RID: 407
		private string u = "HH:mm:ss.ff";

		// Token: 0x04000198 RID: 408
		private bool v;

		// Token: 0x04000199 RID: 409
		private readonly Logger.c w = new Logger.c();

		// Token: 0x02000073 RID: 115
		private sealed class c
		{
			// Token: 0x060003EF RID: 1007 RVA: 0x00009C5D File Offset: 0x00008C5D
			public c()
			{
				this.b = Task.FromResult<IDisposable>(new Logger.c.a(this));
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x00009C84 File Offset: 0x00008C84
			public Task<IDisposable> a()
			{
				Task task = this.a.WaitAsync();
				if (!task.IsCompleted)
				{
					return task.ContinueWith<IDisposable>(new Func<Task, object, IDisposable>(Logger.c.<>c.<>9.a), this.b.Result, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
				}
				return this.b;
			}

			// Token: 0x0400019A RID: 410
			private readonly SemaphoreSlim a = new SemaphoreSlim(1, 1);

			// Token: 0x0400019B RID: 411
			private readonly Task<IDisposable> b;

			// Token: 0x02000074 RID: 116
			private sealed class a : IDisposable
			{
				// Token: 0x060003F1 RID: 1009 RVA: 0x00009CEB File Offset: 0x00008CEB
				internal a(Logger.c A_0)
				{
					this.a = A_0;
				}

				// Token: 0x060003F2 RID: 1010 RVA: 0x00009CFA File Offset: 0x00008CFA
				public void Dispose()
				{
					this.a.a.Release();
				}

				// Token: 0x0400019C RID: 412
				private readonly Logger.c a;
			}
		}
	}
}
