using System;
using System.Collections;
using System.Text;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000178 RID: 376
	internal class SupportClass
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00098880 File Offset: 0x00096A80
		public static byte[] ToByteArray(sbyte[] sbyteArray)
		{
			byte[] array = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (sbyteArray != null)
				{
					array = new byte[sbyteArray.Length];
					for (int i = 0; i < sbyteArray.Length; i++)
					{
						array[i] = (byte)sbyteArray[i];
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return array;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00098914 File Offset: 0x00096B14
		public static byte[] ToByteArray(string sourceString)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			byte[] bytes;
			try
			{
				bytes = Encoding.UTF8.GetBytes(sourceString);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return bytes;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00098990 File Offset: 0x00096B90
		public static sbyte[] ToSByteArray(byte[] byteArray)
		{
			sbyte[] array = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (byteArray != null)
				{
					array = new sbyte[byteArray.Length];
					for (int i = 0; i < byteArray.Length; i++)
					{
						array[i] = (sbyte)byteArray[i];
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
			return array;
		}

		// Token: 0x02000179 RID: 377
		public interface SetSupport : IEnumerable, IList, ICollection
		{
			// Token: 0x06000EA6 RID: 3750
			bool Add(object obj);

			// Token: 0x06000EA7 RID: 3751
			bool AddAll(ICollection c);
		}

		// Token: 0x0200017A RID: 378
		public class ThreadClass : IThreadRunnable
		{
			// Token: 0x06000EA8 RID: 3752 RVA: 0x00098A2C File Offset: 0x00096C2C
			public ThreadClass()
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
				}
				this.threadField = new Thread(new ThreadStart(this.Run));
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}

			// Token: 0x06000EA9 RID: 3753 RVA: 0x00098A88 File Offset: 0x00096C88
			public virtual void Run()
			{
			}

			// Token: 0x06000EAA RID: 3754 RVA: 0x00098A8C File Offset: 0x00096C8C
			public virtual void Start()
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
				}
				this.threadField.Start();
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}

			// Token: 0x170002AE RID: 686
			// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00098ACC File Offset: 0x00096CCC
			// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00098AD4 File Offset: 0x00096CD4
			public Thread Instance
			{
				get
				{
					return this.threadField;
				}
				set
				{
					this.threadField = value;
				}
			}

			// Token: 0x170002AF RID: 687
			// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00098AE0 File Offset: 0x00096CE0
			// (set) Token: 0x06000EAE RID: 3758 RVA: 0x00098AF0 File Offset: 0x00096CF0
			public string Name
			{
				get
				{
					return this.threadField.Name;
				}
				set
				{
					if (this.threadField.Name == null)
					{
						this.threadField.Name = value;
					}
				}
			}

			// Token: 0x170002B0 RID: 688
			// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00098B0C File Offset: 0x00096D0C
			public bool IsAlive
			{
				get
				{
					return this.threadField.IsAlive;
				}
			}

			// Token: 0x170002B1 RID: 689
			// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00098B1C File Offset: 0x00096D1C
			// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00098B2C File Offset: 0x00096D2C
			public bool IsBackground
			{
				get
				{
					return this.threadField.IsBackground;
				}
				set
				{
					this.threadField.IsBackground = value;
				}
			}

			// Token: 0x06000EB2 RID: 3762 RVA: 0x00098B3C File Offset: 0x00096D3C
			public void Join()
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
				}
				this.threadField.Join();
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}

			// Token: 0x06000EB3 RID: 3763 RVA: 0x00098B7C File Offset: 0x00096D7C
			public static SupportClass.ThreadClass Current()
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
				}
				SupportClass.ThreadClass threadClass = new SupportClass.ThreadClass();
				threadClass.Instance = Thread.CurrentThread;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
				return threadClass;
			}

			// Token: 0x040010DC RID: 4316
			private Thread threadField;
		}

		// Token: 0x0200017B RID: 379
		public class CalendarManager
		{
			// Token: 0x040010DD RID: 4317
			public const int YEAR = 1;

			// Token: 0x040010DE RID: 4318
			public const int MONTH = 2;

			// Token: 0x040010DF RID: 4319
			public const int DATE = 5;

			// Token: 0x040010E0 RID: 4320
			public const int HOUR = 10;

			// Token: 0x040010E1 RID: 4321
			public const int MINUTE = 12;

			// Token: 0x040010E2 RID: 4322
			public const int SECOND = 13;

			// Token: 0x040010E3 RID: 4323
			public const int MILLISECOND = 14;

			// Token: 0x040010E4 RID: 4324
			public const int DAY_OF_YEAR = 4;

			// Token: 0x040010E5 RID: 4325
			public const int DAY_OF_MONTH = 6;

			// Token: 0x040010E6 RID: 4326
			public const int DAY_OF_WEEK = 7;

			// Token: 0x040010E7 RID: 4327
			public const int HOUR_OF_DAY = 11;

			// Token: 0x040010E8 RID: 4328
			public const int AM_PM = 9;

			// Token: 0x040010E9 RID: 4329
			public const int AM = 0;

			// Token: 0x040010EA RID: 4330
			public const int PM = 1;
		}
	}
}
