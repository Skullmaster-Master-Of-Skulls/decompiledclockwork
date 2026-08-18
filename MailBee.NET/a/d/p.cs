using System;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using a.n;
using MailBee;
using MailBee.AddressCheck;
using MailBee.DnsMX;
using MailBee.Mime;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x0200041B RID: 1051
	internal class p : bc, global::a.d.g
	{
		// Token: 0x060024A8 RID: 9384 RVA: 0x0009C0F4 File Offset: 0x0009B0F4
		public p(bo A_0, bc A_1, Logger A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.a = null;
			this.b = null;
			this.c = null;
			this.d = null;
			this.e = null;
			this.f = 0;
			this.g = 0;
			this.j = false;
			this.k = false;
			this.l = false;
			this.m = false;
			this.n = false;
			this.o = null;
			this.q = new ae();
			this.r = 1;
			this.p = null;
			this.t = null;
			this.s = null;
			this.u = null;
			this.h = new ManualResetEvent(true);
			this.i = null;
			this.v = null;
			this.w = null;
			if (this.b != null)
			{
				this.v = (p.g)Delegate.Combine(this.v, new p.g(this.a));
				this.w = (p.b)Delegate.Combine(this.w, new p.b(this.a));
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0009C202 File Offset: 0x0009B202
		public override string er()
		{
			return "BULK";
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0009C209 File Offset: 0x0009B209
		protected override void fw(MailBeeException A_0)
		{
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x0009C20C File Offset: 0x0009B20C
		public override void fx()
		{
			base.fx();
			this.j = true;
			aw aw = this.o;
			if (aw != null)
			{
				for (int i = 0; i < aw.Count; i++)
				{
					aw.a(i).fx();
				}
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0009C250 File Offset: 0x0009B250
		public new void a(string A_0, MailMessage A_1, string A_2, bool A_3, string A_4, EmailAddressCollection A_5, DeliveryNotificationOptions A_6, Smtp8bitDataConversion A_7, bool A_8, bool A_9, bool A_10, SendFailureThreshold A_11, int A_12)
		{
			if (A_4 == null && A_1 != null)
			{
				A_4 = A_1.From.Email;
			}
			if (A_5 == null && A_1 != null)
			{
				A_5 = A_1.GetAllRecipients();
			}
			this.e(new SendMailJob(A_0, A_1, A_2, A_3, A_4, A_5, A_6, null, null, null, null, null, A_7, A_8, A_9, A_10, AddressValidationLevel.OK, null, -1, null, A_11, A_12, true, false));
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0009C2B0 File Offset: 0x0009B2B0
		public new void a(string A_0, MailMessage A_1, string A_2, EmailAddressCollection A_3, DeliveryNotificationOptions A_4, DataTable A_5, object A_6, IDataReader A_7, string[] A_8, Smtp8bitDataConversion A_9, bool A_10, bool A_11, bool A_12, AddressValidationLevel A_13, string A_14, int A_15, Regex A_16, SendFailureThreshold A_17, int A_18, bool A_19, bool A_20)
		{
			this.e(new SendMailJob(A_0, A_1, null, false, A_2, A_3, A_4, A_5, A_6, A_7, A_8, null, A_9, A_10, A_11, A_12, A_13, A_14, A_15, A_16, A_17, A_18, A_19, A_20));
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0009C2F4 File Offset: 0x0009B2F4
		private new ManualResetEvent a(global::a.d.f A_0, Thread[] A_1, int A_2)
		{
			Interlocked.Increment(ref this.f);
			ManualResetEvent manualResetEvent = new ManualResetEvent(false);
			Thread thread = new Thread(new ThreadStart(new p.h(A_0, this, manualResetEvent).a));
			A_1[A_2] = thread;
			thread.Name = A_0.bb().ToString("X2");
			thread.Start();
			return manualResetEvent;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0009C354 File Offset: 0x0009B354
		private new int a(int A_0)
		{
			if (this.b.Count > A_0)
			{
				return this.b.Count;
			}
			for (int i = 0; i < this.b.Count; i++)
			{
				if (this.b[i].MergeTable != null || this.b[i].MergeDataReader != null)
				{
					return A_0;
				}
			}
			return this.b.Count;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0009C3C4 File Offset: 0x0009B3C4
		public new void l()
		{
			this.k = false;
			this.l = false;
			this.h.Reset();
			this.i = null;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0009C3E8 File Offset: 0x0009B3E8
		public new void e()
		{
			WaitHandle[] array = null;
			WaitHandle[] a_ = null;
			try
			{
				int num = this.r;
				if (num < 0 || num > 60)
				{
					num = 60;
				}
				this.d.b(string.Format(Resources.Instance.Log_MailingProcessingJobsStarted, new object[0]), null, LogMessageType.Info, this);
				this.o = new aw();
				int num2 = this.a(num);
				if (num2 > 0)
				{
					int num3;
					base.a(num2, num, this.q, out num3, out array, out a_);
					if (num3 == 0)
					{
						global::a.d.f f = new global::a.d.f(this.b, this, this.d, 0);
						this.b(f);
						f.a(base.ba());
						this.a(f, false);
					}
					else
					{
						for (int i = 0; i < num3; i++)
						{
							global::a.d.f f = new global::a.d.f(this.b, this, this.d, i);
							this.b(f);
							this.a(f);
							f.hc(this.k);
							f.hd(this.l);
							this.o.a(f);
						}
						base.a(num, num3, this.q);
						this.p = new Thread[num3];
						for (int j = 0; j < num3; j++)
						{
							global::a.d.f f = (global::a.d.f)this.o.a(j);
							array[j] = this.a(f, this.p, j);
							Thread.Sleep(1);
						}
						for (int k = 0; k < num3; k++)
						{
							base.a(this.o, this.p, array, a_, k);
						}
						this.p = null;
					}
				}
				this.o = null;
				this.d.b(string.Format(Resources.Instance.Log_MailingProcessingJobsFinished, new object[0]), null, LogMessageType.Info, this);
			}
			finally
			{
				if (this.i != null)
				{
					o o = this.i;
					o.b();
					this.h.Set();
					if (o.a() != null)
					{
						try
						{
							o.a().DynamicInvoke(new object[]
							{
								o
							});
						}
						catch (TargetInvocationException)
						{
						}
					}
				}
			}
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0009C624 File Offset: 0x0009B624
		public void r()
		{
			if (this.j)
			{
				throw new MailBeeUserAbortException(5);
			}
			if (this.l)
			{
				throw new MailBeeBatchException(13);
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x0009C648 File Offset: 0x0009B648
		private new SendMailJob g(SendMailJob A_0)
		{
			SendMailJob sendMailJob = A_0;
			if (A_0.MergeTable != null || A_0.MergeDataReader != null)
			{
				if ((A_0.MergeTable != null && A_0.IsEmptyMergeDataTableJob) || (A_0.MergeDataReader != null && !A_0.MergeDataReader.Read()))
				{
					sendMailJob = null;
				}
				else
				{
					sendMailJob = A_0.f();
					if (sendMailJob == A_0 && !A_0.KeepProducedJobs)
					{
						A_0.KeepProducedJobs = true;
					}
				}
			}
			return sendMailJob;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x0009C6AC File Offset: 0x0009B6AC
		private new void f(SendMailJob A_0)
		{
			if (A_0.MessageFilename != null)
			{
				A_0.ab();
			}
			else if (A_0.MergedMessage == null || A_0.KeepMergedData)
			{
				if (A_0.OriginalBcc != null)
				{
					A_0.Message.Bcc.Add(A_0.OriginalBcc);
				}
			}
			else
			{
				A_0.aa();
			}
			A_0.OriginalBcc = null;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x0009C708 File Offset: 0x0009B708
		private new void e(SendMailJob A_0)
		{
			if (this.a)
			{
				object obj = this.a;
				lock (obj)
				{
					this.b.a(A_0);
					goto IL_3D;
				}
			}
			this.b.a(A_0);
			IL_3D:
			if (this.d.Enabled)
			{
				this.d.b(string.Format(Resources.Instance.Log_MailingPendingJobEnqueued, new object[0]) + string.Format(Resources.Instance.LogSuffix_Tag0Rows1, A_0.Tag, A_0.GetIndicesAsString()), null, LogMessageType.Info, this);
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x0009C7B4 File Offset: 0x0009B7B4
		private new void b(SendMailJob A_0, bool A_1)
		{
			if (this.r == 1 || !A_1)
			{
				this.d(A_0);
			}
			else
			{
				object obj = this.a;
				lock (obj)
				{
					this.d(A_0);
				}
			}
			if (this.d.Enabled)
			{
				this.d.b(string.Format(Resources.Instance.Log_MailingFailedJobReEnqueued, new object[0]) + string.Format(Resources.Instance.LogSuffix_Tag0Rows1, A_0.Tag, A_0.GetIndicesAsString()), null, LogMessageType.Info, this);
			}
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x0009C85C File Offset: 0x0009B85C
		private new void d(SendMailJob A_0)
		{
			this.c.b(A_0);
			this.f(A_0);
			this.b.c(A_0);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x0009C880 File Offset: 0x0009B880
		public void p()
		{
			if (this.r == 1)
			{
				this.b();
			}
			else
			{
				object obj = this.a;
				lock (obj)
				{
					this.b();
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_MailingFailedJobsEnqueued, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0009C8F4 File Offset: 0x0009B8F4
		private new void b()
		{
			while (this.d.Count > 0)
			{
				SendMailJob a_ = this.d[0];
				this.d.a(0);
				this.b.a(a_);
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0009C938 File Offset: 0x0009B938
		private new SendMailJob b(bool A_0)
		{
			SendMailJob sendMailJob = null;
			if (this.r == 1 || !A_0)
			{
				sendMailJob = this.a();
			}
			else
			{
				object obj = this.a;
				lock (obj)
				{
					sendMailJob = this.a();
				}
			}
			if (sendMailJob != null && this.d.Enabled)
			{
				this.d.b(string.Format(Resources.Instance.Log_MailingPendingJobWentToProcessing, new object[0]) + string.Format(Resources.Instance.LogSuffix_Tag0Rows1, sendMailJob.Tag, sendMailJob.GetIndicesAsString()), null, LogMessageType.Info, this);
				if (this.n)
				{
					this.d.b(string.Format(Resources.Instance.Log_MailingNoPendingJobsLeft, new object[0]), null, LogMessageType.Info, this);
				}
			}
			return sendMailJob;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0009CA10 File Offset: 0x0009BA10
		private new SendMailJob a()
		{
			SendMailJob sendMailJob = null;
			if (this.b.Count > 0)
			{
				SendMailJob sendMailJob2 = this.b[0];
				n n = this.b as n;
				MailMessage a_ = (n == null) ? null : n.p();
				if (sendMailJob2.a(a_))
				{
					sendMailJob = sendMailJob2;
				}
				else
				{
					sendMailJob = this.g(sendMailJob2);
				}
				if (sendMailJob == null)
				{
					this.b.a(0);
					return this.a();
				}
				if (sendMailJob == sendMailJob2)
				{
					this.b.a(0);
					this.n = (this.b.Count == 0);
				}
				else
				{
					this.n = false;
				}
				this.c.a(sendMailJob);
			}
			return sendMailJob;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0009CABC File Offset: 0x0009BABC
		private new void b(SendMailJob A_0, bool A_1, global::a.d.c A_2)
		{
			bool a_ = A_0.KeepProducedJobs;
			if (this.b != null && this.b.bq() && ((o)this.b).mp() && !this.b.bf())
			{
				a_ = A_2.et(A_0);
				A_0.KeepProducedJobs = a_;
			}
			if (this.r == 1 || !A_1)
			{
				this.c(A_0);
			}
			else
			{
				object obj = this.a;
				lock (obj)
				{
					this.c(A_0);
				}
			}
			if (this.d.Enabled)
			{
				string arg = A_0.IsMessageSent ? Resources.Instance.LogParam_MailingSucceeded : Resources.Instance.LogParam_MailingFailedOrCancelled;
				this.d.b(string.Format(Resources.Instance.Log_MailingJob0, arg) + string.Format(Resources.Instance.LogSuffix_Tag0Rows1, A_0.Tag, A_0.GetIndicesAsString()), null, LogMessageType.Info, this);
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0009CBC8 File Offset: 0x0009BBC8
		private new void c(SendMailJob A_0)
		{
			this.c.b(A_0);
			this.f(A_0);
			if (A_0.KeepProducedJobs)
			{
				if (A_0.ErrorReason == null)
				{
					this.e.a(A_0);
					return;
				}
				this.d.a(A_0);
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0009CC06 File Offset: 0x0009BC06
		private new void b(global::a.d.f A_0)
		{
			A_0.a(this.t);
			A_0.a(this.s);
			A_0.hc(this.k);
			A_0.hd(this.l);
			A_0.a(this.u);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0009CC44 File Offset: 0x0009BC44
		private new void a(global::a.d.f A_0)
		{
			if (this.g >= this.t.h())
			{
				this.g = 0;
			}
			A_0.a(this.g);
			this.g++;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0009CC7C File Offset: 0x0009BC7C
		private new void b(SendMailJob A_0)
		{
			n n = this.b as n;
			string a_ = string.Empty;
			if (n != null)
			{
				a_ = n.n();
			}
			A_0.OriginalBcc = global::a.d.a.a(A_0.ActualMessage, ref a_);
			if (n != null)
			{
				n.l(a_);
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0009CCC4 File Offset: 0x0009BCC4
		private new void a(SendMailJob A_0)
		{
			if (this.r == 1)
			{
				this.b(A_0);
				return;
			}
			object obj = this.a;
			lock (obj)
			{
				this.b(A_0);
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0009CD18 File Offset: 0x0009BD18
		private new bool a(SendMailJob A_0, bool A_1, global::a.d.g A_2)
		{
			if (A_0.IsMergeWithDataRowNeeded)
			{
				if (this.b != null && this.b.bq() && ((o)this.b).mj() && !this.b.bf() && !A_2.es(A_0.Message, (A_0.SenderEmail == null && A_0.ValidationLevel == AddressValidationLevel.OK) ? A_0.Message.From.Email : A_0.SenderEmail, (A_0.Recipients == null && A_0.ValidationLevel == AddressValidationLevel.OK) ? A_0.Message.GetAllRecipients() : A_0.Recipients, (A_0.ValidationLevel == AddressValidationLevel.OK) ? A_0.DsnSettings : null, new k(A_0.MergeTable, A_0.MergeRowIndex, A_0.MergeDataReader, A_0.MergeDataReaderColumnNames, A_0.MergeDataReaderRowValues), A_0.Tag, (A_0.ValidationLevel == AddressValidationLevel.OK) ? null : new global::a.n.a(A_0.ValidationLevel, A_0.EmailColumnName, A_0.EmailColumnIndex, A_0.SyntaxCheck)))
				{
					A_0.Cancelled = true;
					return false;
				}
				if (this.r == 1 || !A_1)
				{
					A_0.d();
				}
				else
				{
					object obj = this.a;
					lock (obj)
					{
						A_0.d();
					}
				}
			}
			return true;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x0009CE80 File Offset: 0x0009BE80
		public new void a(global::a.d.f A_0, bool A_1)
		{
			bool flag = false;
			SendMailJob sendMailJob;
			while (!this.j && !this.k && (sendMailJob = this.b(true)) != null)
			{
				flag = true;
				try
				{
					if (this.a(sendMailJob, true, A_0))
					{
						if (sendMailJob.ValidationLevel == AddressValidationLevel.OK)
						{
							this.a(sendMailJob);
						}
						A_0.a(sendMailJob.ActualMessage, sendMailJob.ActualSenderEmail, sendMailJob.ActualRecipients, sendMailJob.ActualDsnSettings, sendMailJob.Conversion8to7bit, sendMailJob.DoSmtpConnection, sendMailJob.SubmitSenderAndRecipients, sendMailJob.SendData, sendMailJob.FailureThreshold, sendMailJob.MaxThreadCount, this.q, true, new k(sendMailJob.MergeTable, sendMailJob.MergeRowIndex, sendMailJob.MergeDataReader, sendMailJob.MergeDataReaderColumnNames, sendMailJob.MergeDataReaderRowValues), sendMailJob.Tag, (sendMailJob.ValidationLevel == AddressValidationLevel.OK) ? null : new global::a.n.a(sendMailJob.ValidationLevel, sendMailJob.EmailColumnName, sendMailJob.EmailColumnIndex, sendMailJob.SyntaxCheck));
						sendMailJob.IsMessageSentInternal = true;
						sendMailJob.ErrorReasonInternal = null;
					}
				}
				catch (MailBeeException a_)
				{
					A_0.c(a_);
					if (A_0.i())
					{
						sendMailJob.IsMessageSentInternal = true;
						sendMailJob.ErrorReasonInternal = null;
					}
					else
					{
						sendMailJob.ErrorReasonInternal = a_;
						if (this.m)
						{
							this.k = true;
							this.l = true;
						}
						if (A_0.b() != null && A_0.b().ao())
						{
							A_0.b().a(A_0.ba());
							A_0.b().@as();
						}
					}
				}
				finally
				{
					if (A_0.i() || sendMailJob.Cancelled || !this.m)
					{
						this.b(sendMailJob, true, A_0);
					}
					else
					{
						this.b(sendMailJob, true);
					}
					A_0.h();
				}
			}
			if (A_0 != null && A_0.b() != null && A_0.b().ao())
			{
				A_0.b().a(A_0.ba());
				try
				{
					A_0.b().fz(true);
				}
				catch (MailBeeException a_2)
				{
					A_0.b().c(a_2);
					A_0.b().@as();
				}
			}
			if (A_1)
			{
				if (flag)
				{
					this.d.b(string.Format(Resources.Instance.Log_MailingWorkerThreadDone, Thread.CurrentThread.GetHashCode().ToString("X4"), A_0.bb().ToString("X2")), null, LogMessageType.Info, this);
				}
				Interlocked.Decrement(ref this.f);
				base.a(this.r, -1, this.q);
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0009D130 File Offset: 0x0009C130
		public new void a(string A_0, bool A_1)
		{
			n n = this.b as n;
			global::a.d.f f = new global::a.d.f(this.b, this, this.d, 0);
			f.a(base.ba());
			this.d.b(string.Format(Resources.Instance.Log_MailingProcessingJobsStarted, new object[0]), null, LogMessageType.Info, this);
			SendMailJob sendMailJob;
			while (!this.j && !this.k && (sendMailJob = this.b(false)) != null)
			{
				try
				{
					if (this.a(sendMailJob, false, this))
					{
						string a_ = string.Empty;
						if (n != null)
						{
							a_ = n.n();
						}
						try
						{
							sendMailJob.IsMessageSentInternal = (f.a(sendMailJob.ActualMessage, A_0, null, sendMailJob.ActualSenderEmail, sendMailJob.ActualRecipients, A_1, new k(sendMailJob.MergeTable, sendMailJob.MergeRowIndex, sendMailJob.MergeDataReader, sendMailJob.MergeDataReaderColumnNames, sendMailJob.MergeDataReaderRowValues), sendMailJob.Tag, ref a_) != null);
						}
						finally
						{
							if (n != null)
							{
								n.l(a_);
							}
						}
					}
				}
				catch (MailBeeException a_2)
				{
					base.c(a_2);
					sendMailJob.ErrorReasonInternal = a_2;
					if (this.m)
					{
						this.k = true;
						this.l = true;
					}
				}
				finally
				{
					if (f.i() || sendMailJob.Cancelled || !this.m)
					{
						this.b(sendMailJob, false, f);
					}
					else
					{
						this.b(sendMailJob, false);
					}
				}
			}
			this.d.b(string.Format(Resources.Instance.Log_MailingProcessingJobsFinished, new object[0]), null, LogMessageType.Info, this);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x0009D2D0 File Offset: 0x0009C2D0
		public new SmtpServerCollection f()
		{
			return this.t;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0009D2D8 File Offset: 0x0009C2D8
		public new void a(SmtpServerCollection A_0)
		{
			this.t = A_0;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0009D2E1 File Offset: 0x0009C2E1
		public new DnsServerCollection j()
		{
			return this.s;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0009D2E9 File Offset: 0x0009C2E9
		public new void a(DnsServerCollection A_0)
		{
			this.s = A_0;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0009D2F2 File Offset: 0x0009C2F2
		public DirectSendServerConfig q()
		{
			return this.u;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x0009D2FA File Offset: 0x0009C2FA
		public new void a(DirectSendServerConfig A_0)
		{
			this.u = A_0;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0009D303 File Offset: 0x0009C303
		public bool s()
		{
			return this.m;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0009D30B File Offset: 0x0009C30B
		public new void c(bool A_0)
		{
			this.m = A_0;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0009D314 File Offset: 0x0009C314
		public void v()
		{
			this.k = true;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0009D320 File Offset: 0x0009C320
		public bool es(MailMessage A_0, string A_1, EmailAddressCollection A_2, DeliveryNotificationOptions A_3, k A_4, string A_5, global::a.n.a A_6)
		{
			if (this.v != null)
			{
				SmtpMergingMessageEventArgs smtpMergingMessageEventArgs = new SmtpMergingMessageEventArgs(A_0, A_1, A_2, A_3, A_4, A_5, A_6, this);
				base.a(this.v, new object[]
				{
					smtpMergingMessageEventArgs,
					this
				});
				return smtpMergingMessageEventArgs.MergeIt;
			}
			return true;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0009D36C File Offset: 0x0009C36C
		public new void a(SmtpMergingMessageEventArgs A_0, bc A_1)
		{
			o o = (o)this.b;
			if (this.b.bq() && o.mj() && !this.b.bf())
			{
				o.mk(A_0);
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0009D3B0 File Offset: 0x0009C3B0
		public bool h(SendMailJob A_0)
		{
			if (this.w != null)
			{
				SmtpFinishingJobEventArgs smtpFinishingJobEventArgs = new SmtpFinishingJobEventArgs(A_0, this);
				base.a(this.w, new object[]
				{
					smtpFinishingJobEventArgs,
					this
				});
				return smtpFinishingJobEventArgs.KeepIt;
			}
			return true;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0009D3F0 File Offset: 0x0009C3F0
		public new void a(SmtpFinishingJobEventArgs A_0, bc A_1)
		{
			o o = (o)this.b;
			if (this.b.bq() && o.mp() && !this.b.bf())
			{
				o.mq(A_0);
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0009D432 File Offset: 0x0009C432
		public bool n()
		{
			return this.l;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0009D43A File Offset: 0x0009C43A
		public new WaitHandle d()
		{
			return this.h;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0009D442 File Offset: 0x0009C442
		public new o c()
		{
			return this.i;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0009D44A File Offset: 0x0009C44A
		public new void a(o A_0)
		{
			this.i = A_0;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0009D453 File Offset: 0x0009C453
		public new ae g()
		{
			return this.q;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0009D45B File Offset: 0x0009C45B
		public new void a(ae A_0)
		{
			this.q = A_0;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0009D464 File Offset: 0x0009C464
		public int t()
		{
			return this.r;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0009D46C File Offset: 0x0009C46C
		public new void b(int A_0)
		{
			this.r = A_0;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0009D475 File Offset: 0x0009C475
		public new object k()
		{
			return this.a;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x0009D47D File Offset: 0x0009C47D
		public new void a(object A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x0009D486 File Offset: 0x0009C486
		public new SendMailJobCollection m()
		{
			return this.b;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x0009D48E File Offset: 0x0009C48E
		public new void c(SendMailJobCollection A_0)
		{
			this.b = A_0;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x0009D497 File Offset: 0x0009C497
		public SendMailJobCollection u()
		{
			return this.c;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x0009D49F File Offset: 0x0009C49F
		public new void d(SendMailJobCollection A_0)
		{
			this.c = A_0;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0009D4A8 File Offset: 0x0009C4A8
		public SendMailJobCollection h()
		{
			return this.d;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0009D4B0 File Offset: 0x0009C4B0
		public new void b(SendMailJobCollection A_0)
		{
			this.d = A_0;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0009D4B9 File Offset: 0x0009C4B9
		public new SendMailJobCollection i()
		{
			return this.e;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0009D4C1 File Offset: 0x0009C4C1
		public new void a(SendMailJobCollection A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0009D4CA File Offset: 0x0009C4CA
		protected override Task f1(MailBeeException A_0)
		{
			return Task.FromResult<int>(0);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0009D4D4 File Offset: 0x0009C4D4
		private new Task a(SendMailJob A_0, bool A_1, global::a.d.c A_2)
		{
			p.d d;
			d.d = this;
			d.c = A_0;
			d.f = A_1;
			d.e = A_2;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = d.b;
			asyncTaskMethodBuilder.Start<p.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0009D534 File Offset: 0x0009C534
		private new Task a(SendMailJob A_0, bool A_1)
		{
			p.e e;
			e.c = this;
			e.e = A_0;
			e.d = A_1;
			e.b = AsyncTaskMethodBuilder.Create();
			e.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = e.b;
			asyncTaskMethodBuilder.Start<p.e>(ref e);
			return e.b.Task;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0009D58C File Offset: 0x0009C58C
		private new Task<SendMailJob> a(bool A_0)
		{
			p.i i;
			i.c = this;
			i.d = A_0;
			i.b = AsyncTaskMethodBuilder<SendMailJob>.Create();
			i.a = -1;
			AsyncTaskMethodBuilder<SendMailJob> asyncTaskMethodBuilder = i.b;
			asyncTaskMethodBuilder.Start<p.i>(ref i);
			return i.b.Task;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0009D5DC File Offset: 0x0009C5DC
		public Task o()
		{
			p.f f;
			f.c = this;
			f.b = AsyncTaskMethodBuilder.Create();
			f.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = f.b;
			asyncTaskMethodBuilder.Start<p.f>(ref f);
			return f.b.Task;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0009D624 File Offset: 0x0009C624
		public new Task b(global::a.d.f A_0, bool A_1)
		{
			p.a a;
			a.c = this;
			a.e = A_0;
			a.g = A_1;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<p.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0009D67C File Offset: 0x0009C67C
		public new Task b(string A_0, bool A_1)
		{
			p.c c;
			c.c = this;
			c.g = A_0;
			c.h = A_1;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = c.b;
			asyncTaskMethodBuilder.Start<p.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x0400184F RID: 6223
		private new object a;

		// Token: 0x04001850 RID: 6224
		private new SendMailJobCollection b;

		// Token: 0x04001851 RID: 6225
		private new SendMailJobCollection c;

		// Token: 0x04001852 RID: 6226
		private new SendMailJobCollection d;

		// Token: 0x04001853 RID: 6227
		private new SendMailJobCollection e;

		// Token: 0x04001854 RID: 6228
		private new int f;

		// Token: 0x04001855 RID: 6229
		private new int g;

		// Token: 0x04001856 RID: 6230
		private ManualResetEvent h;

		// Token: 0x04001857 RID: 6231
		private new o i;

		// Token: 0x04001858 RID: 6232
		private new bool j;

		// Token: 0x04001859 RID: 6233
		private new bool k;

		// Token: 0x0400185A RID: 6234
		private new bool l;

		// Token: 0x0400185B RID: 6235
		private new bool m;

		// Token: 0x0400185C RID: 6236
		private bool n;

		// Token: 0x0400185D RID: 6237
		private aw o;

		// Token: 0x0400185E RID: 6238
		private Thread[] p;

		// Token: 0x0400185F RID: 6239
		private ae q;

		// Token: 0x04001860 RID: 6240
		private int r;

		// Token: 0x04001861 RID: 6241
		private DnsServerCollection s;

		// Token: 0x04001862 RID: 6242
		private SmtpServerCollection t;

		// Token: 0x04001863 RID: 6243
		private DirectSendServerConfig u;

		// Token: 0x04001864 RID: 6244
		private p.g v;

		// Token: 0x04001865 RID: 6245
		private p.b w;

		// Token: 0x0200041D RID: 1053
		internal class h
		{
			// Token: 0x060024EC RID: 9452 RVA: 0x0009D6D1 File Offset: 0x0009C6D1
			public h(global::a.d.f A_0, p A_1, ManualResetEvent A_2)
			{
				this.a = A_0;
				this.b = A_1;
				this.c = A_2;
			}

			// Token: 0x060024ED RID: 9453 RVA: 0x0009D6F0 File Offset: 0x0009C6F0
			public void a()
			{
				try
				{
					this.b.a(this.a, true);
				}
				catch (Exception ex)
				{
					this.b.a8().WriteLine(ex.ToString());
				}
				finally
				{
					this.c.Set();
				}
			}

			// Token: 0x04001866 RID: 6246
			private global::a.d.f a;

			// Token: 0x04001867 RID: 6247
			private p b;

			// Token: 0x04001868 RID: 6248
			private ManualResetEvent c;
		}

		// Token: 0x0200041E RID: 1054
		// (Invoke) Token: 0x060024EF RID: 9455
		protected new delegate void g(SmtpMergingMessageEventArgs A_0, bc A_1);

		// Token: 0x0200041F RID: 1055
		// (Invoke) Token: 0x060024F3 RID: 9459
		protected new delegate void b(SmtpFinishingJobEventArgs A_0, bc A_1);
	}
}
