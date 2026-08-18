using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MailBee;
using MailBee.AntiSpam;

namespace a.m
{
	// Token: 0x02000210 RID: 528
	internal class e
	{
		// Token: 0x06001140 RID: 4416 RVA: 0x0004B488 File Offset: 0x0004A488
		public string b()
		{
			return this.g;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0004B490 File Offset: 0x0004A490
		public e(BayesFilter A_0)
		{
			this.h = A_0;
			this.e = false;
			this.f = new Hashtable(100000, 0.1f);
			this.g = Guid.NewGuid().ToString();
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0004B4E0 File Offset: 0x0004A4E0
		public void a(string A_0)
		{
			this.e = false;
			this.f.Clear();
			StreamReader streamReader = null;
			bool flag = false;
			if (this.h.OnLockedDatabase == null)
			{
				for (int i = 0; i < 100; i++)
				{
					try
					{
						streamReader = new StreamReader(File.Open(A_0, FileMode.Open, FileAccess.Read, FileShare.Read));
						flag = true;
					}
					catch (FileNotFoundException a_)
					{
						throw new MailBeeIOException(31, a_);
					}
					catch (IOException)
					{
						flag = false;
					}
					if (flag)
					{
						break;
					}
					Thread.Sleep(100);
				}
			}
			else if (!this.h.OnLockedDatabase(A_0, false))
			{
				return;
			}
			try
			{
				if (!flag)
				{
					try
					{
						streamReader = new StreamReader(File.Open(A_0, FileMode.Open, FileAccess.Read, FileShare.Read));
					}
					catch (IOException a_2)
					{
						throw new MailBeeIOException(30, a_2);
					}
				}
				string text = streamReader.ReadLine();
				if (text != null)
				{
					this.g = text;
					string key;
					while ((key = streamReader.ReadLine()) != null)
					{
						string s = streamReader.ReadLine();
						string s2 = streamReader.ReadLine();
						int a_3;
						long fileTime;
						try
						{
							a_3 = int.Parse(s);
							fileTime = long.Parse(s2);
						}
						catch (ArgumentNullException a_4)
						{
							throw new MailBeeIOException(44, a_4);
						}
						catch (FormatException a_5)
						{
							throw new MailBeeIOException(44, a_5);
						}
						catch (OverflowException a_6)
						{
							throw new MailBeeIOException(44, a_6);
						}
						DateTime a_7;
						try
						{
							a_7 = DateTime.FromFileTime(fileTime);
						}
						catch (ArgumentOutOfRangeException a_8)
						{
							throw new MailBeeIOException(23, a_8);
						}
						this.f.Add(key, new global::a.m.e.e(a_3, a_7));
					}
				}
			}
			catch (FileNotFoundException)
			{
			}
			catch (IOException a_9)
			{
				throw new MailBeeIOException(30, a_9);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0004B6B8 File Offset: 0x0004A6B8
		public void a(Stream A_0)
		{
			this.e = false;
			this.f.Clear();
			StreamReader streamReader = null;
			try
			{
				try
				{
					streamReader = new StreamReader(A_0);
				}
				catch (ArgumentException a_)
				{
					throw new MailBeeIOException(20, a_);
				}
				string text = streamReader.ReadLine();
				if (text != null)
				{
					this.g = text;
					string key;
					while ((key = streamReader.ReadLine()) != null)
					{
						string s = streamReader.ReadLine();
						string s2 = streamReader.ReadLine();
						int a_2;
						long fileTime;
						try
						{
							a_2 = int.Parse(s);
							fileTime = long.Parse(s2);
						}
						catch (ArgumentNullException a_3)
						{
							throw new MailBeeIOException(44, a_3);
						}
						catch (FormatException a_4)
						{
							throw new MailBeeIOException(44, a_4);
						}
						catch (OverflowException a_5)
						{
							throw new MailBeeIOException(44, a_5);
						}
						DateTime a_6;
						try
						{
							a_6 = DateTime.FromFileTime(fileTime);
						}
						catch (ArgumentOutOfRangeException a_7)
						{
							throw new MailBeeIOException(23, a_7);
						}
						this.f.Add(key, new global::a.m.e.e(a_2, a_6));
					}
				}
			}
			catch (IOException a_8)
			{
				throw new MailBeeIOException(30, a_8);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0004B7F4 File Offset: 0x0004A7F4
		public void a(string A_0, bool A_1)
		{
			if (!this.e && !A_1)
			{
				return;
			}
			StreamWriter streamWriter = null;
			try
			{
				if (this.h.OnLockedDatabase == null)
				{
					for (int i = 0; i < 100; i++)
					{
						for (int j = 0; j < 100; j++)
						{
							try
							{
								streamWriter = new StreamWriter(File.Open(A_0, FileMode.Create, FileAccess.Write, FileShare.None));
							}
							catch (UnauthorizedAccessException)
							{
								if (j == 99)
								{
									throw;
								}
							}
							catch (IOException)
							{
								if (j == 99)
								{
									throw;
								}
							}
							if (streamWriter != null)
							{
								break;
							}
						}
						if (streamWriter != null)
						{
							break;
						}
						if (i == 99)
						{
							streamWriter = new StreamWriter(File.Open(A_0, FileMode.Create, FileAccess.Write, FileShare.None));
						}
						Thread.Sleep(100);
					}
				}
				else
				{
					if (!this.h.OnLockedDatabase(A_0, false))
					{
						return;
					}
					streamWriter = new StreamWriter(File.Open(A_0, FileMode.Create, FileAccess.Write, FileShare.None));
				}
				streamWriter.WriteLine(this.g);
				IDictionaryEnumerator enumerator = this.f.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::a.m.e.e e = (global::a.m.e.e)enumerator.Value;
					string value = (string)enumerator.Key;
					streamWriter.WriteLine(value);
					streamWriter.WriteLine(e.a.ToString());
					streamWriter.WriteLine(e.b.ToFileTime().ToString());
				}
			}
			catch (UnauthorizedAccessException a_)
			{
				throw new MailBeeIOException(32, a_);
			}
			catch (DirectoryNotFoundException a_2)
			{
				throw new MailBeeIOException(31, a_2);
			}
			catch (IOException a_3)
			{
				throw new MailBeeIOException(30, a_3);
			}
			finally
			{
				if (streamWriter != null)
				{
					streamWriter.Close();
				}
				this.e = false;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0004B9E4 File Offset: 0x0004A9E4
		public void a(Stream A_0, bool A_1)
		{
			if (!this.e && !A_1)
			{
				return;
			}
			StreamWriter streamWriter = null;
			try
			{
				try
				{
					streamWriter = new StreamWriter(A_0);
				}
				catch (ArgumentException a_)
				{
					throw new MailBeeIOException(20, a_);
				}
				streamWriter.WriteLine(this.g);
				IDictionaryEnumerator enumerator = this.f.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::a.m.e.e e = (global::a.m.e.e)enumerator.Value;
					string value = (string)enumerator.Key;
					streamWriter.WriteLine(value);
					streamWriter.WriteLine(e.a.ToString());
					streamWriter.WriteLine(e.b.ToFileTime().ToString());
				}
			}
			catch (UnauthorizedAccessException a_2)
			{
				throw new MailBeeIOException(32, a_2);
			}
			catch (DirectoryNotFoundException a_3)
			{
				throw new MailBeeIOException(31, a_3);
			}
			catch (IOException a_4)
			{
				throw new MailBeeIOException(30, a_4);
			}
			finally
			{
				if (streamWriter != null)
				{
					streamWriter.Close();
				}
				this.e = false;
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0004BAF8 File Offset: 0x0004AAF8
		public void c()
		{
			this.f.Clear();
			this.e = true;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0004BB0C File Offset: 0x0004AB0C
		public void a()
		{
			this.a(TimeSpan.FromSeconds(2592000.0));
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0004BB24 File Offset: 0x0004AB24
		public void a(TimeSpan A_0)
		{
			Hashtable hashtable = new Hashtable(100000, 1f);
			DateTime utcNow = DateTime.UtcNow;
			utcNow.Subtract(A_0);
			IDictionaryEnumerator enumerator = this.f.GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::a.m.e.e e = (global::a.m.e.e)enumerator.Value;
				if (DateTime.Compare(e.b, utcNow) >= 0)
				{
					string key = (string)enumerator.Key;
					hashtable.Add(key, e);
				}
			}
			this.f.Clear();
			this.f = hashtable;
			this.e = true;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0004BBB0 File Offset: 0x0004ABB0
		public void b(string A_0)
		{
			this.f.Remove(A_0);
			this.e = true;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0004BBC8 File Offset: 0x0004ABC8
		public bool a(string A_0, out int A_1, out DateTime A_2)
		{
			bool result = false;
			try
			{
				global::a.m.e.e e = (global::a.m.e.e)this.f[A_0];
				if (e != null)
				{
					A_1 = e.a;
					A_2 = e.b;
					result = true;
				}
				else
				{
					A_1 = 0;
					A_2 = DateTime.UtcNow;
				}
			}
			catch (Exception)
			{
				A_1 = 0;
				A_2 = DateTime.UtcNow;
			}
			return result;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0004BC38 File Offset: 0x0004AC38
		public void a(string A_0, int A_1)
		{
			global::a.m.e.e e = null;
			try
			{
				e = (global::a.m.e.e)this.f[A_0];
			}
			catch (Exception)
			{
			}
			if (e == null)
			{
				e = new global::a.m.e.e(A_1, DateTime.UtcNow);
				this.f[A_0] = e;
			}
			else
			{
				e.a = A_1;
				e.b = DateTime.UtcNow;
				this.f[A_0] = e;
			}
			this.e = true;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0004BCB4 File Offset: 0x0004ACB4
		public Task c(string A_0)
		{
			global::a.m.e.b b;
			b.c = this;
			b.d = A_0;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<global::a.m.e.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0004BD04 File Offset: 0x0004AD04
		public Task b(Stream A_0)
		{
			global::a.m.e.d d;
			d.c = this;
			d.d = A_0;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<global::a.m.e.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0004BD54 File Offset: 0x0004AD54
		public Task b(string A_0, bool A_1)
		{
			global::a.m.e.c c;
			c.c = this;
			c.e = A_0;
			c.d = A_1;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<global::a.m.e.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0004BDAC File Offset: 0x0004ADAC
		public Task b(Stream A_0, bool A_1)
		{
			global::a.m.e.a a;
			a.c = this;
			a.e = A_0;
			a.d = A_1;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<global::a.m.e.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04000EC1 RID: 3777
		public const int a = 0;

		// Token: 0x04000EC2 RID: 3778
		public const int b = 1;

		// Token: 0x04000EC3 RID: 3779
		public const int c = 2;

		// Token: 0x04000EC4 RID: 3780
		public const int d = 3;

		// Token: 0x04000EC5 RID: 3781
		private bool e;

		// Token: 0x04000EC6 RID: 3782
		private Hashtable f;

		// Token: 0x04000EC7 RID: 3783
		private string g;

		// Token: 0x04000EC8 RID: 3784
		private BayesFilter h;

		// Token: 0x04000EC9 RID: 3785
		private const int i = 100;

		// Token: 0x04000ECA RID: 3786
		private const int j = 100;

		// Token: 0x02000211 RID: 529
		private class e
		{
			// Token: 0x06001150 RID: 4432 RVA: 0x0004BE01 File Offset: 0x0004AE01
			public e(int A_0, DateTime A_1)
			{
				this.a = A_0;
				this.b = A_1;
			}

			// Token: 0x04000ECB RID: 3787
			public int a;

			// Token: 0x04000ECC RID: 3788
			public DateTime b;
		}
	}
}
