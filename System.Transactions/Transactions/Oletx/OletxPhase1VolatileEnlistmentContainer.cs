using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x02000097 RID: 151
	internal class OletxPhase1VolatileEnlistmentContainer : OletxVolatileEnlistmentContainer
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0003D3B4 File Offset: 0x0003C7B4
		internal OletxPhase1VolatileEnlistmentContainer(RealOletxTransaction realOletxTransaction)
		{
			this.voterBallotShim = null;
			this.realOletxTransaction = realOletxTransaction;
			this.phase = -1;
			this.outstandingNotifications = 0;
			this.incompleteDependentClones = 0;
			this.alreadyVoted = false;
			this.collectedVoteYes = true;
			this.enlistmentList = new ArrayList();
			realOletxTransaction.IncrementUndecidedEnlistments();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0003D414 File Offset: 0x0003C814
		internal void AddEnlistment(OletxVolatileEnlistment enlistment)
		{
			lock (this)
			{
				if (-1 != this.phase)
				{
					throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TooLate"), null);
				}
				this.enlistmentList.Add(enlistment);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0003D484 File Offset: 0x0003C884
		internal override void AddDependentClone()
		{
			lock (this)
			{
				if (-1 != this.phase)
				{
					throw TransactionException.CreateTransactionStateException(SR.GetString("TraceSourceOletx"), null);
				}
				this.incompleteDependentClones++;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0003D4F4 File Offset: 0x0003C8F4
		internal override void DependentCloneCompleted()
		{
			if (DiagnosticTrace.Verbose)
			{
				string methodName = string.Concat(new string[]
				{
					"OletxPhase1VolatileEnlistmentContainer.DependentCloneCompleted, outstandingNotifications = ",
					this.outstandingNotifications.ToString(CultureInfo.CurrentCulture),
					", incompleteDependentClones = ",
					this.incompleteDependentClones.ToString(CultureInfo.CurrentCulture),
					", phase = ",
					this.phase.ToString(CultureInfo.CurrentCulture)
				});
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName);
			}
			this.incompleteDependentClones--;
			if (DiagnosticTrace.Verbose)
			{
				string methodName2 = "OletxPhase1VolatileEnlistmentContainer.DependentCloneCompleted";
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName2);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0003D5A4 File Offset: 0x0003C9A4
		internal override void RollbackFromTransaction()
		{
			bool flag = false;
			IVoterBallotShim voterBallotShim = null;
			lock (this)
			{
				if (DiagnosticTrace.Verbose)
				{
					string methodName = "OletxPhase1VolatileEnlistmentContainer.RollbackFromTransaction, outstandingNotifications = " + this.outstandingNotifications.ToString(CultureInfo.CurrentCulture) + ", incompleteDependentClones = " + this.incompleteDependentClones.ToString(CultureInfo.CurrentCulture);
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName);
				}
				if (1 == this.phase && 0 < this.outstandingNotifications)
				{
					this.alreadyVoted = true;
					flag = true;
					voterBallotShim = this.voterBallotShim;
				}
			}
			if (flag)
			{
				try
				{
					if (voterBallotShim != null)
					{
						voterBallotShim.Vote(false);
					}
					this.Aborted();
				}
				catch (COMException ex)
				{
					if (NativeMethods.XACT_E_CONNECTION_DOWN != ex.ErrorCode && NativeMethods.XACT_E_TMNOTAVAILABLE != ex.ErrorCode)
					{
						throw;
					}
					lock (this)
					{
						if (1 == this.phase)
						{
							this.InDoubt();
						}
					}
					if (DiagnosticTrace.Verbose)
					{
						ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), ex);
					}
				}
				finally
				{
					HandleTable.FreeHandle(this.voterHandle);
				}
			}
			if (DiagnosticTrace.Verbose)
			{
				string methodName2 = "OletxPhase1VolatileEnlistmentContainer.RollbackFromTransaction";
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName2);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0003D744 File Offset: 0x0003CB44
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0003D794 File Offset: 0x0003CB94
		internal IVoterBallotShim VoterBallotShim
		{
			get
			{
				IVoterBallotShim result = null;
				lock (this)
				{
					result = this.voterBallotShim;
				}
				return result;
			}
			set
			{
				lock (this)
				{
					this.voterBallotShim = value;
				}
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0003D7E4 File Offset: 0x0003CBE4
		internal override void DecrementOutstandingNotifications(bool voteYes)
		{
			bool flag = false;
			IVoterBallotShim voterBallotShim = null;
			lock (this)
			{
				if (DiagnosticTrace.Verbose)
				{
					string methodName = "OletxPhase1VolatileEnlistmentContainer.DecrementOutstandingNotifications, outstandingNotifications = " + this.outstandingNotifications.ToString(CultureInfo.CurrentCulture) + ", incompleteDependentClones = " + this.incompleteDependentClones.ToString(CultureInfo.CurrentCulture);
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName);
				}
				this.outstandingNotifications--;
				this.collectedVoteYes = (this.collectedVoteYes && voteYes);
				if (this.outstandingNotifications == 0)
				{
					if (1 == this.phase && !this.alreadyVoted)
					{
						flag = true;
						this.alreadyVoted = true;
						voterBallotShim = this.VoterBallotShim;
					}
					this.realOletxTransaction.DecrementUndecidedEnlistments();
				}
			}
			try
			{
				if (flag)
				{
					if (this.collectedVoteYes && !this.realOletxTransaction.Doomed)
					{
						if (voterBallotShim != null)
						{
							voterBallotShim.Vote(true);
						}
					}
					else
					{
						try
						{
							if (voterBallotShim != null)
							{
								voterBallotShim.Vote(false);
							}
							this.Aborted();
						}
						finally
						{
							HandleTable.FreeHandle(this.voterHandle);
						}
					}
				}
			}
			catch (COMException ex)
			{
				if (NativeMethods.XACT_E_CONNECTION_DOWN != ex.ErrorCode && NativeMethods.XACT_E_TMNOTAVAILABLE != ex.ErrorCode)
				{
					throw;
				}
				lock (this)
				{
					if (1 == this.phase)
					{
						this.InDoubt();
					}
				}
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), ex);
				}
			}
			if (DiagnosticTrace.Verbose)
			{
				string methodName2 = "OletxPhase1VolatileEnlistmentContainer.DecrementOutstandingNotifications";
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName2);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0003D9D4 File Offset: 0x0003CDD4
		internal override void OutcomeFromTransaction(TransactionStatus outcome)
		{
			bool flag = false;
			bool flag2 = false;
			lock (this)
			{
				if (1 == this.phase && 0 < this.outstandingNotifications)
				{
					if (TransactionStatus.Aborted == outcome)
					{
						flag = true;
					}
					else if (TransactionStatus.InDoubt == outcome)
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.Aborted();
			}
			if (flag2)
			{
				this.InDoubt();
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0003DA44 File Offset: 0x0003CE44
		internal override void Committed()
		{
			int num = 0;
			lock (this)
			{
				this.phase = 2;
				num = this.enlistmentList.Count;
			}
			for (int i = 0; i < num; i++)
			{
				OletxVolatileEnlistment oletxVolatileEnlistment = this.enlistmentList[i] as OletxVolatileEnlistment;
				if (oletxVolatileEnlistment == null)
				{
					if (DiagnosticTrace.Critical)
					{
						InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
					}
					throw new InvalidOperationException(SR.GetString("InternalError"));
				}
				oletxVolatileEnlistment.Commit();
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0003DAF4 File Offset: 0x0003CEF4
		internal override void Aborted()
		{
			int num = 0;
			lock (this)
			{
				this.phase = 2;
				num = this.enlistmentList.Count;
			}
			for (int i = 0; i < num; i++)
			{
				OletxVolatileEnlistment oletxVolatileEnlistment = this.enlistmentList[i] as OletxVolatileEnlistment;
				if (oletxVolatileEnlistment == null)
				{
					if (DiagnosticTrace.Critical)
					{
						InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
					}
					throw new InvalidOperationException(SR.GetString("InternalError"));
				}
				oletxVolatileEnlistment.Rollback();
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0003DBA4 File Offset: 0x0003CFA4
		internal override void InDoubt()
		{
			int num = 0;
			lock (this)
			{
				this.phase = 2;
				num = this.enlistmentList.Count;
			}
			for (int i = 0; i < num; i++)
			{
				OletxVolatileEnlistment oletxVolatileEnlistment = this.enlistmentList[i] as OletxVolatileEnlistment;
				if (oletxVolatileEnlistment == null)
				{
					if (DiagnosticTrace.Critical)
					{
						InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
					}
					throw new InvalidOperationException(SR.GetString("InternalError"));
				}
				oletxVolatileEnlistment.InDoubt();
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0003DC54 File Offset: 0x0003D054
		internal void VoteRequest()
		{
			int num = 0;
			bool flag = false;
			lock (this)
			{
				if (DiagnosticTrace.Verbose)
				{
					string methodName = "OletxPhase1VolatileEnlistmentContainer.VoteRequest";
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName);
				}
				this.phase = 1;
				if (0 < this.incompleteDependentClones)
				{
					flag = true;
					this.outstandingNotifications = 1;
				}
				else
				{
					this.outstandingNotifications = this.enlistmentList.Count;
					num = this.enlistmentList.Count;
					if (num == 0)
					{
						this.outstandingNotifications = 1;
					}
				}
				this.realOletxTransaction.TooLateForEnlistments = true;
			}
			if (flag)
			{
				this.DecrementOutstandingNotifications(false);
			}
			else if (num == 0)
			{
				this.DecrementOutstandingNotifications(true);
			}
			else
			{
				for (int i = 0; i < num; i++)
				{
					OletxVolatileEnlistment oletxVolatileEnlistment = this.enlistmentList[i] as OletxVolatileEnlistment;
					if (oletxVolatileEnlistment == null)
					{
						if (DiagnosticTrace.Critical)
						{
							InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
						}
						throw new InvalidOperationException(SR.GetString("InternalError"));
					}
					oletxVolatileEnlistment.Prepare(this);
				}
			}
			if (DiagnosticTrace.Verbose)
			{
				string methodName2 = "OletxPhase1VolatileEnlistmentContainer.VoteRequest";
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), methodName2);
			}
		}

		// Token: 0x04000235 RID: 565
		private IVoterBallotShim voterBallotShim;

		// Token: 0x04000236 RID: 566
		internal IntPtr voterHandle = IntPtr.Zero;
	}
}
