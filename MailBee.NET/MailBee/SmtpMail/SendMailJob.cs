using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using MailBee.AddressCheck;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x0200013E RID: 318
	public class SendMailJob
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x0002E41C File Offset: 0x0002D41C
		internal SendMailJob(string A_0, MailMessage A_1, string A_2, bool A_3, string A_4, EmailAddressCollection A_5, DeliveryNotificationOptions A_6, DataTable A_7, object A_8, IDataReader A_9, string[] A_10, object[] A_11, Smtp8bitDataConversion A_12, bool A_13, bool A_14, bool A_15, AddressValidationLevel A_16, string A_17, int A_18, Regex A_19, SendFailureThreshold A_20, int A_21, bool A_22, bool A_23)
		{
			if (A_0 == null)
			{
				this.c = string.Empty;
			}
			else
			{
				this.c = A_0;
			}
			this.d = A_1;
			this.e = A_2;
			this.f = A_3;
			this.g = A_4;
			this.h = A_5;
			this.i = A_6;
			this.j = A_7;
			this.l = A_9;
			this.p = A_10;
			this.o = A_11;
			this.q = -1;
			this.r = A_12;
			this.t = A_14;
			this.s = A_13;
			this.u = A_15;
			this.v = A_16;
			this.w = A_17;
			this.x = A_18;
			this.y = A_19;
			this.z = A_20;
			this.aa = A_21;
			this.m = null;
			if (A_8 == null)
			{
				if (A_7 == null)
				{
					this.k = 0;
				}
				else
				{
					this.k = new int[]
					{
						0,
						-1
					};
				}
			}
			else if (A_8 is int[] || A_8 is int)
			{
				this.k = A_8;
			}
			else
			{
				if (!(A_8 is string))
				{
					throw new MailBeeInvalidArgumentException(20);
				}
				this.k = this.a((string)A_8);
				this.m = (string)A_8;
			}
			if (this.k is int[] && ((int[])this.k).Length == 1)
			{
				this.k = ((int[])this.k)[0];
			}
			this.n = 0;
			this.ab = null;
			this.ac = null;
			this.ad = null;
			this.ae = null;
			this.af = null;
			this.ag = false;
			this.ah = null;
			this.ai = false;
			this.aj = A_22;
			this.ak = A_23;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002E5F0 File Offset: 0x0002D5F0
		internal int a(int[] A_0, int A_1)
		{
			if (this.k is int)
			{
				if (A_0 != null)
				{
					A_0[A_1] = (int)this.k;
				}
				return 1;
			}
			int[] array = (int[])this.k;
			if (A_0 != null)
			{
				Array.Copy(array, this.n, A_0, A_1, array.Length - this.n);
			}
			return array.Length - this.n;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002E650 File Offset: 0x0002D650
		private int[] a(string A_0)
		{
			int[] array = null;
			if (A_0 == string.Empty)
			{
				array = new int[0];
			}
			else
			{
				try
				{
					string[] array2 = A_0.Split(new char[]
					{
						','
					});
					array = new int[array2.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = int.Parse(array2[i]);
					}
				}
				catch
				{
					throw new MailBeeInvalidArgumentException(20);
				}
			}
			return array;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002E6C8 File Offset: 0x0002D6C8
		public string GetIndicesAsString()
		{
			if (this.m == null)
			{
				if (this.k is int)
				{
					this.m = ((int)this.k).ToString();
				}
				else
				{
					int[] array = (int[])this.k;
					string[] array2 = new string[array.Length - this.n];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = array[i + this.n].ToString();
					}
					this.m = string.Join(",", array2);
				}
			}
			return this.m;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002E75C File Offset: 0x0002D75C
		public string Tag
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002E764 File Offset: 0x0002D764
		public MailMessage Message
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002E76C File Offset: 0x0002D76C
		public string MessageFilename
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002E774 File Offset: 0x0002D774
		public bool PreferXSenderXReceiver
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002E77C File Offset: 0x0002D77C
		public MailMessage MergedMessage
		{
			get
			{
				return this.ab;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002E784 File Offset: 0x0002D784
		internal MailMessage ActualMessage
		{
			get
			{
				if (this.ab != null)
				{
					return this.ab;
				}
				return this.d;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002E79B File Offset: 0x0002D79B
		public string SenderEmail
		{
			get
			{
				if (this.g != null)
				{
					return this.g;
				}
				return this.d.From.Email;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002E7BC File Offset: 0x0002D7BC
		public string MergedSenderEmail
		{
			get
			{
				return this.ac;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002E7C4 File Offset: 0x0002D7C4
		internal string ActualSenderEmail
		{
			get
			{
				if (this.ac != null)
				{
					return this.ac;
				}
				return this.g;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002E7DB File Offset: 0x0002D7DB
		public EmailAddressCollection Recipients
		{
			get
			{
				if (this.h != null)
				{
					return this.h;
				}
				return this.d.GetAllRecipients();
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002E7F7 File Offset: 0x0002D7F7
		public EmailAddressCollection MergedRecipients
		{
			get
			{
				return this.ad;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0002E7FF File Offset: 0x0002D7FF
		internal EmailAddressCollection ActualRecipients
		{
			get
			{
				if (this.ad != null)
				{
					return this.ad;
				}
				return this.h;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002E816 File Offset: 0x0002D816
		public DeliveryNotificationOptions DsnSettings
		{
			get
			{
				return this.i;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002E81E File Offset: 0x0002D81E
		public DeliveryNotificationOptions MergedDsnSettings
		{
			get
			{
				return this.ae;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002E826 File Offset: 0x0002D826
		internal DeliveryNotificationOptions ActualDsnSettings
		{
			get
			{
				if (this.ae != null)
				{
					return this.ae;
				}
				return this.i;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002E83D File Offset: 0x0002D83D
		internal void aa()
		{
			if (!this.ak)
			{
				this.ab = null;
				this.ac = null;
				this.ad = null;
				this.ae = null;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002E863 File Offset: 0x0002D863
		internal bool IsMergeWithDataRowNeeded
		{
			get
			{
				return this.ab == null && (this.j != null || this.l != null);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002E882 File Offset: 0x0002D882
		public DataTable MergeTable
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002E88A File Offset: 0x0002D88A
		public IDataReader MergeDataReader
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002E892 File Offset: 0x0002D892
		public string[] MergeDataReaderColumnNames
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002E89A File Offset: 0x0002D89A
		public object[] MergeDataReaderRowValues
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002E8A2 File Offset: 0x0002D8A2
		internal Smtp8bitDataConversion Conversion8to7bit
		{
			get
			{
				return this.r;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0002E8AA File Offset: 0x0002D8AA
		internal bool DoSmtpConnection
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002E8B2 File Offset: 0x0002D8B2
		internal bool SubmitSenderAndRecipients
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002E8BA File Offset: 0x0002D8BA
		internal bool SendData
		{
			get
			{
				return this.u;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002E8C2 File Offset: 0x0002D8C2
		internal AddressValidationLevel ValidationLevel
		{
			get
			{
				return this.v;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002E8CA File Offset: 0x0002D8CA
		internal string EmailColumnName
		{
			get
			{
				return this.w;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002E8D2 File Offset: 0x0002D8D2
		internal int EmailColumnIndex
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0002E8DA File Offset: 0x0002D8DA
		internal Regex SyntaxCheck
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002E8E2 File Offset: 0x0002D8E2
		internal SendFailureThreshold FailureThreshold
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002E8EA File Offset: 0x0002D8EA
		internal int MaxThreadCount
		{
			get
			{
				return this.aa;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002E8F2 File Offset: 0x0002D8F2
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0002E8FA File Offset: 0x0002D8FA
		internal EmailAddressCollection OriginalBcc
		{
			get
			{
				return this.af;
			}
			set
			{
				this.af = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002E903 File Offset: 0x0002D903
		public bool IsMessageSent
		{
			get
			{
				return this.ag;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002E90B File Offset: 0x0002D90B
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x0002E913 File Offset: 0x0002D913
		internal bool IsMessageSentInternal
		{
			get
			{
				return this.ag;
			}
			set
			{
				this.ag = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002E91C File Offset: 0x0002D91C
		public MailBeeException ErrorReason
		{
			get
			{
				return this.ah;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002E924 File Offset: 0x0002D924
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0002E92C File Offset: 0x0002D92C
		internal MailBeeException ErrorReasonInternal
		{
			get
			{
				return this.ah;
			}
			set
			{
				this.ah = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002E935 File Offset: 0x0002D935
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0002E93D File Offset: 0x0002D93D
		internal bool Cancelled
		{
			get
			{
				return this.ai;
			}
			set
			{
				this.ai = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002E946 File Offset: 0x0002D946
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0002E94E File Offset: 0x0002D94E
		internal bool KeepProducedJobs
		{
			get
			{
				return this.aj;
			}
			set
			{
				this.aj = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002E957 File Offset: 0x0002D957
		internal bool KeepMergedData
		{
			get
			{
				return this.ak;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002E95F File Offset: 0x0002D95F
		public int MergeRowIndex
		{
			get
			{
				if (this.k is int)
				{
					return (int)this.k;
				}
				if (((int[])this.k).Length != 0)
				{
					return ((int[])this.k)[this.n];
				}
				return 0;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002E99C File Offset: 0x0002D99C
		internal object MergeRowIndices
		{
			get
			{
				return this.k;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002E9A4 File Offset: 0x0002D9A4
		internal bool IsEmptyMergeDataTableJob
		{
			get
			{
				return this.j == null || this.j.Rows.Count == 0 || (this.k is int[] && ((int[])this.k).Length == 0);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002E9E0 File Offset: 0x0002D9E0
		private SendMailJob a(int A_0, object[] A_1)
		{
			return new SendMailJob(this.Tag, this.Message, this.MessageFilename, this.PreferXSenderXReceiver, this.SenderEmail, this.Recipients, this.DsnSettings, this.MergeTable, A_0, this.MergeDataReader, this.MergeDataReaderColumnNames, A_1, this.Conversion8to7bit, this.DoSmtpConnection, this.SubmitSenderAndRecipients, this.SendData, this.ValidationLevel, this.EmailColumnName, this.EmailColumnIndex, this.SyntaxCheck, this.FailureThreshold, this.MaxThreadCount, this.KeepProducedJobs, this.KeepMergedData);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002EA80 File Offset: 0x0002DA80
		private object[] a()
		{
			object[] array = new object[this.l.FieldCount];
			this.l.GetValues(array);
			return array;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002EAAC File Offset: 0x0002DAAC
		internal SendMailJob f()
		{
			if (this.j != null && this.k is int)
			{
				return this;
			}
			if (this.l != null)
			{
				this.q++;
				return this.a(this.q, this.a());
			}
			this.m = null;
			int[] array = (int[])this.k;
			int num = array[this.n];
			if (array.Length > this.n + 1 && array[this.n + 1] < 0)
			{
				if (num < this.j.Rows.Count - 1)
				{
					array[this.n]++;
					return this.a(num, null);
				}
				this.n += 2;
				if (this.n >= array.Length)
				{
					this.k = num;
					this.n = 0;
					return this;
				}
				return this.a(num, null);
			}
			else
			{
				this.n++;
				if (this.n >= array.Length)
				{
					this.k = num;
					this.n = 0;
					return this;
				}
				return this.a(num, null);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002EBD0 File Offset: 0x0002DBD0
		internal void d()
		{
			DataRow dataRow = null;
			int mergeRowIndex = this.MergeRowIndex;
			if (this.j != null)
			{
				try
				{
					dataRow = this.j.Rows[mergeRowIndex];
				}
				catch (IndexOutOfRangeException)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
			}
			if (this.v == AddressValidationLevel.OK)
			{
				bool a_ = this.i.TrackingID != null && this.i.TrackingID != string.Empty;
				string email = this.g;
				EmailAddressCollection emailAddressCollection = null;
				this.ae = this.i.a();
				if (this.h != null)
				{
					emailAddressCollection = new EmailAddressCollection(this.h.ToString());
				}
				if (this.j != null)
				{
					using (IEnumerator enumerator = this.j.Columns.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DataColumn dataColumn = (DataColumn)obj;
							this.a(dataColumn.ColumnName, dataRow[dataColumn].ToString(), ref email, a_, emailAddressCollection);
						}
						goto IL_18F;
					}
				}
				if (this.o != null && this.p != null)
				{
					for (int i = 0; i < this.o.Length; i++)
					{
						this.a(this.p[i], this.o[i].ToString(), ref email, a_, emailAddressCollection);
					}
				}
				IL_18F:
				this.ab = this.d.Merge.MergedMessage;
				this.d.Merge.Reset();
				if (email == null)
				{
					email = this.ab.From.Email;
				}
				this.ac = email;
				if (emailAddressCollection == null)
				{
					emailAddressCollection = this.ab.GetAllRecipients();
				}
				this.ad = emailAddressCollection;
				return;
			}
			if (this.j != null)
			{
				this.ad = new EmailAddressCollection(dataRow[this.w].ToString());
				return;
			}
			if (this.l != null)
			{
				this.ad = new EmailAddressCollection(this.o[this.x].ToString());
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002EDE4 File Offset: 0x0002DDE4
		private void a(string A_0, string A_1, ref string A_2, bool A_3, EmailAddressCollection A_4)
		{
			string text = "##" + A_0 + "##";
			this.d.Merge.Replace(text, A_1);
			if (A_3)
			{
				this.ae.TrackingID = this.ae.TrackingID.Replace(text, A_1);
			}
			if (A_2 != null)
			{
				A_2 = A_2.Replace(text, A_1);
			}
			if (A_4 != null)
			{
				A_4.AsString = A_4.ToString().Replace(text, A_1);
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002EE60 File Offset: 0x0002DE60
		internal bool a(MailMessage A_0)
		{
			if (this.e != null)
			{
				this.d = new MailMessage();
				this.d.LoadMessage(this.e);
				if (A_0 != null)
				{
					this.d.Builder = A_0.Builder.a(this.d);
				}
				if (this.f && this.d.Headers["x-sender"] != null)
				{
					this.g = EmailAddress.Parse(this.d.Headers["x-sender"]).Email;
				}
				if (this.g == null)
				{
					this.g = this.d.From.Email;
				}
				if (this.f && this.d.Headers["x-receiver"] != null)
				{
					CollectionBase collectionBase = this.d.Headers.Items("x-receiver");
					this.h = new EmailAddressCollection();
					foreach (object obj in collectionBase)
					{
						Header header = (Header)obj;
						this.h.AddFromString(header.Value);
					}
				}
				if (this.h == null)
				{
					this.h = this.d.GetAllRecipients();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002EFC4 File Offset: 0x0002DFC4
		internal void ab()
		{
			if (this.e != null)
			{
				this.d = null;
			}
		}

		// Token: 0x040007E1 RID: 2017
		private const string a = "##";

		// Token: 0x040007E2 RID: 2018
		private const string b = "##";

		// Token: 0x040007E3 RID: 2019
		private string c;

		// Token: 0x040007E4 RID: 2020
		private MailMessage d;

		// Token: 0x040007E5 RID: 2021
		private string e;

		// Token: 0x040007E6 RID: 2022
		private bool f;

		// Token: 0x040007E7 RID: 2023
		private string g;

		// Token: 0x040007E8 RID: 2024
		private EmailAddressCollection h;

		// Token: 0x040007E9 RID: 2025
		private DeliveryNotificationOptions i;

		// Token: 0x040007EA RID: 2026
		private DataTable j;

		// Token: 0x040007EB RID: 2027
		private object k;

		// Token: 0x040007EC RID: 2028
		private IDataReader l;

		// Token: 0x040007ED RID: 2029
		private string m;

		// Token: 0x040007EE RID: 2030
		private int n;

		// Token: 0x040007EF RID: 2031
		private object[] o;

		// Token: 0x040007F0 RID: 2032
		private string[] p;

		// Token: 0x040007F1 RID: 2033
		private int q;

		// Token: 0x040007F2 RID: 2034
		private Smtp8bitDataConversion r;

		// Token: 0x040007F3 RID: 2035
		private bool s;

		// Token: 0x040007F4 RID: 2036
		private bool t;

		// Token: 0x040007F5 RID: 2037
		private bool u;

		// Token: 0x040007F6 RID: 2038
		private AddressValidationLevel v;

		// Token: 0x040007F7 RID: 2039
		private string w;

		// Token: 0x040007F8 RID: 2040
		private int x;

		// Token: 0x040007F9 RID: 2041
		private Regex y;

		// Token: 0x040007FA RID: 2042
		private SendFailureThreshold z;

		// Token: 0x040007FB RID: 2043
		private int aa;

		// Token: 0x040007FC RID: 2044
		private MailMessage ab;

		// Token: 0x040007FD RID: 2045
		private string ac;

		// Token: 0x040007FE RID: 2046
		private EmailAddressCollection ad;

		// Token: 0x040007FF RID: 2047
		private DeliveryNotificationOptions ae;

		// Token: 0x04000800 RID: 2048
		private EmailAddressCollection af;

		// Token: 0x04000801 RID: 2049
		private bool ag;

		// Token: 0x04000802 RID: 2050
		private MailBeeException ah;

		// Token: 0x04000803 RID: 2051
		private bool ai;

		// Token: 0x04000804 RID: 2052
		private bool aj;

		// Token: 0x04000805 RID: 2053
		private bool ak;
	}
}
