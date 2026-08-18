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
	// Token: 0x02000208 RID: 520
	internal class c
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x00047DC7 File Offset: 0x00046DC7
		public string a()
		{
			return this.h;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00047DCF File Offset: 0x00046DCF
		public int b()
		{
			return this.a;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00047DD7 File Offset: 0x00046DD7
		public int c()
		{
			return this.b;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00047DDF File Offset: 0x00046DDF
		public void b(int A_0, bool A_1)
		{
			if (A_1)
			{
				this.a += A_0;
				return;
			}
			this.a -= A_0;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00047E01 File Offset: 0x00046E01
		public void a(int A_0, bool A_1)
		{
			if (A_1)
			{
				this.b += A_0;
				return;
			}
			this.b -= A_0;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00047E23 File Offset: 0x00046E23
		public void c(int A_0)
		{
			this.d = A_0;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00047E2C File Offset: 0x00046E2C
		public void b(int A_0)
		{
			this.e = A_0;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00047E38 File Offset: 0x00046E38
		public c(BayesFilter A_0)
		{
			this.i = A_0;
			this.a = 0;
			this.b = 0;
			this.c = false;
			this.d = 3;
			this.e = 25;
			this.g = 0;
			this.f = new Hashtable(100000, 0.1f);
			this.h = Guid.NewGuid().ToString();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00047EAC File Offset: 0x00046EAC
		public void b(string A_0)
		{
			this.c = false;
			this.g = 0;
			this.a = 0;
			this.b = 0;
			this.f.Clear();
			StreamReader streamReader = null;
			bool flag = false;
			if (this.i.OnLockedDatabase == null)
			{
				for (int i = 0; i < 50; i++)
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
					Thread.Sleep(20);
				}
			}
			else if (!this.i.OnLockedDatabase(A_0, true))
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
				this.h = streamReader.ReadLine();
				string s = streamReader.ReadLine();
				string s2 = streamReader.ReadLine();
				string s3 = streamReader.ReadLine();
				try
				{
					this.g = int.Parse(s);
					this.a = int.Parse(s2);
					this.b = int.Parse(s3);
					goto IL_17C;
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
				IL_11E:
				string s4 = streamReader.ReadLine();
				string s5 = streamReader.ReadLine();
				int a_6;
				int a_7;
				try
				{
					a_6 = int.Parse(s4);
					a_7 = int.Parse(s5);
				}
				catch (ArgumentNullException a_8)
				{
					throw new MailBeeIOException(44, a_8);
				}
				catch (FormatException a_9)
				{
					throw new MailBeeIOException(44, a_9);
				}
				catch (OverflowException a_10)
				{
					throw new MailBeeIOException(44, a_10);
				}
				string key;
				this.f.Add(key, new global::a.m.c.e(a_7, a_6));
				IL_17C:
				if ((key = streamReader.ReadLine()) != null)
				{
					goto IL_11E;
				}
			}
			catch (FileNotFoundException)
			{
			}
			catch (IOException a_11)
			{
				throw new MailBeeIOException(30, a_11);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00048180 File Offset: 0x00047180
		public void a(Stream A_0)
		{
			this.c = false;
			this.g = 0;
			this.a = 0;
			this.b = 0;
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
				this.h = streamReader.ReadLine();
				string s = streamReader.ReadLine();
				string s2 = streamReader.ReadLine();
				string s3 = streamReader.ReadLine();
				try
				{
					this.g = int.Parse(s);
					this.a = int.Parse(s2);
					this.b = int.Parse(s3);
					goto IL_10A;
				}
				catch (ArgumentNullException a_2)
				{
					throw new MailBeeIOException(44, a_2);
				}
				catch (FormatException a_3)
				{
					throw new MailBeeIOException(44, a_3);
				}
				catch (OverflowException a_4)
				{
					throw new MailBeeIOException(44, a_4);
				}
				IL_AC:
				string s4 = streamReader.ReadLine();
				string s5 = streamReader.ReadLine();
				int a_5;
				int a_6;
				try
				{
					a_5 = int.Parse(s4);
					a_6 = int.Parse(s5);
				}
				catch (ArgumentNullException a_7)
				{
					throw new MailBeeIOException(44, a_7);
				}
				catch (FormatException a_8)
				{
					throw new MailBeeIOException(44, a_8);
				}
				catch (OverflowException a_9)
				{
					throw new MailBeeIOException(44, a_9);
				}
				string key;
				this.f.Add(key, new global::a.m.c.e(a_6, a_5));
				IL_10A:
				if ((key = streamReader.ReadLine()) != null)
				{
					goto IL_AC;
				}
			}
			catch (IOException a_10)
			{
				throw new MailBeeIOException(30, a_10);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004832C File Offset: 0x0004732C
		public void a(string A_0, bool A_1)
		{
			if (!this.c && !A_1)
			{
				return;
			}
			StreamWriter streamWriter = null;
			try
			{
				if (this.i.OnLockedDatabase == null)
				{
					for (int i = 0; i < 50; i++)
					{
						for (int j = 0; j < 50; j++)
						{
							try
							{
								streamWriter = new StreamWriter(File.Open(A_0, FileMode.Create, FileAccess.Write, FileShare.None));
							}
							catch (UnauthorizedAccessException)
							{
								if (j == 49)
								{
									throw;
								}
							}
							catch (IOException)
							{
								if (j == 49)
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
						if (i == 49)
						{
							streamWriter = new StreamWriter(File.Open(A_0, FileMode.Create, FileAccess.Write, FileShare.None));
						}
						Thread.Sleep(20);
					}
				}
				else
				{
					if (!this.i.OnLockedDatabase(A_0, true))
					{
						return;
					}
					streamWriter = new StreamWriter(File.Open(A_0, FileMode.Create, FileAccess.Write, FileShare.None));
				}
				streamWriter.WriteLine(this.h);
				streamWriter.WriteLine(this.g.ToString());
				streamWriter.WriteLine(this.a.ToString());
				streamWriter.WriteLine(this.b.ToString());
				IDictionaryEnumerator enumerator = this.f.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::a.m.c.e e = (global::a.m.c.e)enumerator.Value;
					string text = (string)enumerator.Key;
					if (text.IndexOf("\n") == -1)
					{
						streamWriter.WriteLine(text);
						streamWriter.WriteLine(e.b.ToString());
						streamWriter.WriteLine(e.a.ToString());
					}
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
				this.c = false;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00048554 File Offset: 0x00047554
		public void a(Stream A_0, bool A_1)
		{
			if (!this.c && !A_1)
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
				streamWriter.WriteLine(this.h);
				streamWriter.WriteLine(this.g.ToString());
				streamWriter.WriteLine(this.a.ToString());
				streamWriter.WriteLine(this.b.ToString());
				IDictionaryEnumerator enumerator = this.f.GetEnumerator();
				while (enumerator.MoveNext())
				{
					global::a.m.c.e e = (global::a.m.c.e)enumerator.Value;
					string text = (string)enumerator.Key;
					if (text.IndexOf("\n") == -1)
					{
						streamWriter.WriteLine(text);
						streamWriter.WriteLine(e.b.ToString());
						streamWriter.WriteLine(e.a.ToString());
					}
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
				this.c = false;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000486A0 File Offset: 0x000476A0
		private bool a(string A_0, out string A_1)
		{
			if (A_0.Length > this.e)
			{
				ulong num = 0UL;
				for (int i = 0; i < A_0.Length; i++)
				{
					num = (num << 5) + num + (ulong)A_0[i];
				}
				A_1 = num.ToString("X");
				return true;
			}
			A_1 = null;
			return false;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000486F4 File Offset: 0x000476F4
		private bool a(string A_0, int A_1, int A_2, bool A_3)
		{
			global::a.m.c.e e = (global::a.m.c.e)this.f[A_0];
			if (e != null || !A_3)
			{
				if (A_3)
				{
					e.a += A_1;
					e.b += A_2;
				}
				else
				{
					e.a -= A_1;
					e.b -= A_2;
				}
				this.f[A_0] = e;
				return true;
			}
			if (A_3)
			{
				e = new global::a.m.c.e(A_1, A_2);
				this.f[A_0] = e;
				return true;
			}
			return false;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00048788 File Offset: 0x00047788
		public void a(string A_0, int A_1, int A_2)
		{
			if (A_1 + A_2 == 0)
			{
				return;
			}
			if ((ulong)this.a(A_0) < (ulong)((long)this.d))
			{
				return;
			}
			string a_;
			if (this.a(A_0, out a_))
			{
				this.c = (this.a(a_, A_1, A_2, true) || this.c);
				return;
			}
			this.c = (this.a(A_0, A_1, A_2, true) || this.c);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000487F0 File Offset: 0x000477F0
		public void b(string A_0, int A_1, int A_2)
		{
			if (A_1 + A_2 == 0 || (ulong)this.a(A_0) < (ulong)((long)this.d))
			{
				return;
			}
			string a_;
			if (this.a(A_0, out a_))
			{
				this.c = (this.a(a_, A_1, A_2, false) || this.c);
				return;
			}
			this.c = (this.a(A_0, A_1, A_2, false) || this.c);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00048858 File Offset: 0x00047858
		private uint a(string A_0)
		{
			int length = A_0.Length;
			if (length > 1 && '*' == A_0[1])
			{
				return (uint)(length - 2);
			}
			return (uint)length;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00048880 File Offset: 0x00047880
		public bool a(string A_0, out int A_1, out int A_2)
		{
			global::a.m.c.e e = (global::a.m.c.e)this.f[A_0];
			if (e == null)
			{
				A_1 = 0;
				A_2 = 0;
				return false;
			}
			A_1 = e.a;
			A_2 = e.b;
			return true;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000488BB File Offset: 0x000478BB
		public void d()
		{
			this.f.Clear();
			this.c = true;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000488D0 File Offset: 0x000478D0
		public void a(int A_0)
		{
			Hashtable hashtable = new Hashtable(100000, 1f);
			IDictionaryEnumerator enumerator = this.f.GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::a.m.c.e e = (global::a.m.c.e)enumerator.Value;
				if (e.b + e.a >= A_0)
				{
					string key = (string)enumerator.Key;
					hashtable.Add(key, e);
				}
			}
			this.f.Clear();
			this.f = hashtable;
			this.c = true;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0004894C File Offset: 0x0004794C
		public Task c(string A_0)
		{
			global::a.m.c.b b;
			b.c = this;
			b.d = A_0;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<global::a.m.c.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0004899C File Offset: 0x0004799C
		public Task b(Stream A_0)
		{
			global::a.m.c.c c;
			c.c = this;
			c.d = A_0;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<global::a.m.c.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000489EC File Offset: 0x000479EC
		public Task b(string A_0, bool A_1)
		{
			global::a.m.c.a a;
			a.c = this;
			a.e = A_0;
			a.d = A_1;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<global::a.m.c.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00048A44 File Offset: 0x00047A44
		public Task b(Stream A_0, bool A_1)
		{
			global::a.m.c.d d;
			d.c = this;
			d.e = A_0;
			d.d = A_1;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<global::a.m.c.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x04000E6D RID: 3693
		private int a;

		// Token: 0x04000E6E RID: 3694
		private int b;

		// Token: 0x04000E6F RID: 3695
		private bool c;

		// Token: 0x04000E70 RID: 3696
		private int d;

		// Token: 0x04000E71 RID: 3697
		private int e;

		// Token: 0x04000E72 RID: 3698
		private Hashtable f;

		// Token: 0x04000E73 RID: 3699
		private int g;

		// Token: 0x04000E74 RID: 3700
		private string h;

		// Token: 0x04000E75 RID: 3701
		private BayesFilter i;

		// Token: 0x04000E76 RID: 3702
		private const int j = 20;

		// Token: 0x04000E77 RID: 3703
		private const int k = 50;

		// Token: 0x02000209 RID: 521
		private class e
		{
			// Token: 0x0600111F RID: 4383 RVA: 0x00048A99 File Offset: 0x00047A99
			public e(int A_0, int A_1)
			{
				this.a = A_0;
				this.b = A_1;
			}

			// Token: 0x04000E78 RID: 3704
			public int a;

			// Token: 0x04000E79 RID: 3705
			public int b;
		}
	}
}
