using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using a;
using a.m;
using MailBee.Mime;

namespace MailBee.AntiSpam
{
	// Token: 0x02000127 RID: 295
	public class BayesFilter
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x0002BA24 File Offset: 0x0002AA24
		internal void b()
		{
			this.p = 0;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002BA2D File Offset: 0x0002AA2D
		internal void a(int A_0)
		{
			this.p = A_0;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0002BA36 File Offset: 0x0002AA36
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0002BA3E File Offset: 0x0002AA3E
		public LockedDatabaseDelegate OnLockedDatabase
		{
			get
			{
				return this.t;
			}
			set
			{
				this.t = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0002BA47 File Offset: 0x0002AA47
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0002BA4F File Offset: 0x0002AA4F
		public bool AutoLearning
		{
			get
			{
				return this.e;
			}
			set
			{
				this.e = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0002BA58 File Offset: 0x0002AA58
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0002BA60 File Offset: 0x0002AA60
		public int AutoLearningGradeAbove
		{
			get
			{
				return this.f;
			}
			set
			{
				if (value < 0 || value > 100)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				this.f = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002BA7A File Offset: 0x0002AA7A
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0002BA82 File Offset: 0x0002AA82
		public int AutoLearningGradeBelow
		{
			get
			{
				return this.g;
			}
			set
			{
				if (value < 0 || value > 100)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				this.g = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002BA9C File Offset: 0x0002AA9C
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0002BAA4 File Offset: 0x0002AAA4
		public BayesAlgorithm Algorithm
		{
			get
			{
				return this.j;
			}
			set
			{
				this.j = value;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002BAAD File Offset: 0x0002AAAD
		public BayesFilter() : this(null)
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002BAB8 File Offset: 0x0002AAB8
		public BayesFilter(string licenseKey)
		{
			BayesFilter.a(licenseKey);
			this.a = 3;
			this.b = 25;
			this.c = 20;
			this.d = true;
			this.e = false;
			this.f = 90;
			this.g = 5;
			this.h = true;
			this.i = true;
			this.j = BayesAlgorithm.ChiSquareAlgorithm;
			this.k = true;
			this.l = true;
			this.m = true;
			this.n = 0.0178;
			this.o = 0.52;
			this.p = 0;
			this.q = new Logger(null);
			this.q.Filename = "bayesflt.log";
			this.q.Enabled = false;
			this.r = new global::a.m.c(this);
			this.r.c(this.a);
			this.r.b(this.b);
			this.s = new global::a.m.e(this);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002BBB8 File Offset: 0x0002ABB8
		public Task LoadDatabaseAsync(string spamFilename, string nonSpamFilename)
		{
			BayesFilter.b b;
			b.e = this;
			b.c = spamFilename;
			b.d = nonSpamFilename;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<BayesFilter.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002BC10 File Offset: 0x0002AC10
		public Task LoadDatabaseAsync(Stream spamStream, Stream nonSpamStream)
		{
			BayesFilter.c c;
			c.e = this;
			c.c = spamStream;
			c.d = nonSpamStream;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<BayesFilter.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002BC65 File Offset: 0x0002AC65
		public Task SaveDatabaseAsync(string spamFilename, string nonSpamFilename)
		{
			return this.SaveDatabaseAsync(spamFilename, nonSpamFilename, 0, true);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002BC71 File Offset: 0x0002AC71
		public Task SaveDatabaseAsync(Stream spamStream, Stream nonSpamStream)
		{
			return this.SaveDatabaseAsync(spamStream, nonSpamStream, 0, true);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002BC80 File Offset: 0x0002AC80
		public Task SaveDatabaseAsync(string spamFilename, string nonSpamFilename, int threshold, bool saveAlways)
		{
			BayesFilter.d d;
			d.f = this;
			d.c = spamFilename;
			d.d = nonSpamFilename;
			d.e = threshold;
			d.g = saveAlways;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<BayesFilter.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002BCE8 File Offset: 0x0002ACE8
		public Task SaveDatabaseAsync(Stream spamStream, Stream nonSpamStream, int threshold, bool saveAlways)
		{
			BayesFilter.a a;
			a.f = this;
			a.c = spamStream;
			a.d = nonSpamStream;
			a.e = threshold;
			a.g = saveAlways;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<BayesFilter.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002BD50 File Offset: 0x0002AD50
		public void LoadDatabase(string spamFilename, string nonSpamFilename)
		{
			if (spamFilename == null || nonSpamFilename == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			spamFilename = spamFilename.Trim();
			nonSpamFilename = nonSpamFilename.Trim();
			if (spamFilename == string.Empty || nonSpamFilename == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.r = new global::a.m.c(this);
			this.r.c(this.a);
			this.r.b(this.b);
			this.r.b(spamFilename);
			this.s = new global::a.m.e(this);
			this.s.a(nonSpamFilename);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002BDF0 File Offset: 0x0002ADF0
		public void LoadDatabase(Stream spamStream, Stream nonSpamStream)
		{
			if (spamStream == null || nonSpamStream == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.r = new global::a.m.c(this);
			this.r.c(this.a);
			this.r.b(this.b);
			this.r.a(spamStream);
			this.s = new global::a.m.e(this);
			this.s.a(nonSpamStream);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002BE5D File Offset: 0x0002AE5D
		public void SaveDatabase(string spamFilename, string nonSpamFilename)
		{
			this.SaveDatabase(spamFilename, nonSpamFilename, 0, true);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002BE69 File Offset: 0x0002AE69
		public void SaveDatabase(Stream spamStream, Stream nonSpamStream)
		{
			this.SaveDatabase(spamStream, nonSpamStream, 0, true);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002BE78 File Offset: 0x0002AE78
		public void SaveDatabase(string spamFilename, string nonSpamFilename, int threshold, bool saveAlways)
		{
			if (spamFilename == null || nonSpamFilename == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			spamFilename = spamFilename.Trim();
			nonSpamFilename = nonSpamFilename.Trim();
			if (spamFilename == string.Empty || nonSpamFilename == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (threshold > 0)
			{
				this.r.a(threshold);
			}
			this.r.a(spamFilename, saveAlways);
			if (threshold > 0)
			{
				this.s.a();
			}
			this.s.a(nonSpamFilename, saveAlways);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002BF00 File Offset: 0x0002AF00
		public void SaveDatabase(Stream spamStream, Stream nonSpamStream, int threshold, bool saveAlways)
		{
			if (spamStream == null || nonSpamStream == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (threshold > 0)
			{
				this.r.a(threshold);
			}
			this.r.a(spamStream, saveAlways);
			if (threshold > 0)
			{
				this.s.a();
			}
			this.s.a(nonSpamStream, saveAlways);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002BF58 File Offset: 0x0002AF58
		public int ScoreMessage(MailMessage message)
		{
			if (message == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			int num = 50;
			global::a.m.f f = null;
			try
			{
				if (message.Size == 0)
				{
					return 0;
				}
				f = new global::a.m.f(this, message, false);
				f.b(this.k);
				f.a(this.a);
				f.c(this.h);
				f.a(this.i);
				global::a.m.d d = new global::a.m.d(this, this.r);
				d.a(this.j);
				d.a(this.d);
				d.a(this.c);
				d.c(this.o);
				d.b(this.n);
				ArrayList arrayList = f.d();
				if (arrayList.Count == 0)
				{
					return 0;
				}
				if (this.l)
				{
					d.b(ref arrayList);
				}
				num = d.a(arrayList, !this.l);
				return num;
			}
			catch (Exception)
			{
				this.a(2);
			}
			if (!this.e && f != null)
			{
				try
				{
					if (num > this.f)
					{
						this.s.a(message.MessageID, 3);
					}
					else
					{
						this.s.a(message.MessageID, 2);
					}
				}
				catch (Exception)
				{
					this.a(2);
				}
			}
			return 50;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002C0B4 File Offset: 0x0002B0B4
		public void TrainFilter(MailMessage message, bool isSpam)
		{
			if (message == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			try
			{
				if (message.Size != 0)
				{
					global::a.m.f f = new global::a.m.f(this, message, true);
					f.b(this.k);
					f.a(this.a);
					f.c(this.h);
					f.a(this.i);
					global::a.m.d d = new global::a.m.d(this, this.r);
					d.a(this.j);
					d.a(this.d);
					d.a(this.c);
					d.c(this.o);
					d.b(this.n);
					ArrayList arrayList = f.d();
					if (arrayList.Count != 0)
					{
						bool flag = false;
						bool flag2 = false;
						int num;
						DateTime dateTime;
						if (this.s.a(message.MessageID, out num, out dateTime))
						{
							if (num == 0 && isSpam)
							{
								flag = true;
							}
							else if (1 == num && !isSpam)
							{
								flag2 = true;
							}
							else if (2 == num && isSpam)
							{
								this.s.b(message.MessageID);
							}
							else if (3 == num && !isSpam)
							{
								this.s.b(message.MessageID);
							}
						}
						if (!isSpam)
						{
							if (flag2)
							{
								this.r.b(1, false);
								for (int i = 0; i < arrayList.Count; i++)
								{
									global::a.m.a a = (global::a.m.a)arrayList[i];
									this.r.b(a.a, 0, 1);
								}
								this.s.b(message.MessageID);
							}
							this.r.a(1, true);
							if (this.m && flag2)
							{
								int num2;
								do
								{
									for (int j = 0; j < arrayList.Count; j++)
									{
										global::a.m.a a2 = (global::a.m.a)arrayList[j];
										this.r.a(a2.a, 1, 0);
									}
									if (this.l)
									{
										d.b(ref arrayList);
									}
									num2 = d.a(arrayList, !this.l);
								}
								while (num2 >= this.f);
							}
							else
							{
								for (int k = 0; k < arrayList.Count; k++)
								{
									global::a.m.a a3 = (global::a.m.a)arrayList[k];
									this.r.a(a3.a, 1, 0);
								}
							}
							this.s.a(message.MessageID, 0);
						}
						else
						{
							if (flag)
							{
								this.r.a(1, false);
								for (int l = 0; l < arrayList.Count; l++)
								{
									global::a.m.a a4 = (global::a.m.a)arrayList[l];
									this.r.b(a4.a, 1, 0);
								}
								this.s.b(message.MessageID);
							}
							this.r.b(1, true);
							if (this.m && flag)
							{
								int num3;
								do
								{
									for (int m = 0; m < arrayList.Count; m++)
									{
										global::a.m.a a5 = (global::a.m.a)arrayList[m];
										this.r.a(a5.a, 0, 1);
									}
									if (this.l)
									{
										d.b(ref arrayList);
									}
									num3 = d.a(arrayList, !this.l);
								}
								while (num3 < this.f);
							}
							else
							{
								for (int n = 0; n < arrayList.Count; n++)
								{
									global::a.m.a a6 = (global::a.m.a)arrayList[n];
									this.r.a(a6.a, 0, 1);
								}
							}
							this.s.a(message.MessageID, 1);
						}
					}
				}
			}
			catch (Exception)
			{
				this.a(2);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002C450 File Offset: 0x0002B450
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0002C45C File Offset: 0x0002B45C
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = bn.a(value, typeof(BayesFilter));
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0002C473 File Offset: 0x0002B473
		internal static bm License
		{
			get
			{
				return Global.u;
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002C47A File Offset: 0x0002B47A
		internal static void a(string A_0)
		{
			Global.a(typeof(BayesFilter), A_0);
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0002C48C File Offset: 0x0002B48C
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x04000768 RID: 1896
		private int a;

		// Token: 0x04000769 RID: 1897
		private int b;

		// Token: 0x0400076A RID: 1898
		private int c;

		// Token: 0x0400076B RID: 1899
		private bool d;

		// Token: 0x0400076C RID: 1900
		private bool e;

		// Token: 0x0400076D RID: 1901
		private int f;

		// Token: 0x0400076E RID: 1902
		private int g;

		// Token: 0x0400076F RID: 1903
		private bool h;

		// Token: 0x04000770 RID: 1904
		private bool i;

		// Token: 0x04000771 RID: 1905
		private BayesAlgorithm j;

		// Token: 0x04000772 RID: 1906
		private bool k;

		// Token: 0x04000773 RID: 1907
		private bool l;

		// Token: 0x04000774 RID: 1908
		private bool m;

		// Token: 0x04000775 RID: 1909
		private double n;

		// Token: 0x04000776 RID: 1910
		private double o;

		// Token: 0x04000777 RID: 1911
		private int p;

		// Token: 0x04000778 RID: 1912
		private Logger q;

		// Token: 0x04000779 RID: 1913
		private global::a.m.c r;

		// Token: 0x0400077A RID: 1914
		private global::a.m.e s;

		// Token: 0x0400077B RID: 1915
		private LockedDatabaseDelegate t;
	}
}
