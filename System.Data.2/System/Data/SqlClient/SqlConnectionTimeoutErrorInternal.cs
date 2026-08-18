using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001C3 RID: 451
	internal class SqlConnectionTimeoutErrorInternal
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000C0FE8 File Offset: 0x000C03E8
		internal SqlConnectionTimeoutErrorPhase CurrentPhase
		{
			get
			{
				return this.currentPhase;
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000C0FFC File Offset: 0x000C03FC
		public SqlConnectionTimeoutErrorInternal()
		{
			this.phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
			for (int i = 0; i < this.phaseDurations.Length; i++)
			{
				this.phaseDurations[i] = null;
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000C1038 File Offset: 0x000C0438
		public void SetFailoverScenario(bool useFailoverServer)
		{
			this.isFailoverScenario = useFailoverServer;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000C104C File Offset: 0x000C044C
		public void SetInternalSourceType(SqlConnectionInternalSourceType sourceType)
		{
			this.currentSourceType = sourceType;
			if (this.currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
			{
				this.originalPhaseDurations = this.phaseDurations;
				this.phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
				this.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000C108C File Offset: 0x000C048C
		internal void ResetAndRestartPhase()
		{
			this.currentPhase = SqlConnectionTimeoutErrorPhase.PreLoginBegin;
			for (int i = 0; i < this.phaseDurations.Length; i++)
			{
				this.phaseDurations[i] = null;
			}
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000C10BC File Offset: 0x000C04BC
		internal void SetAndBeginPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this.currentPhase = timeoutErrorPhase;
			if (this.phaseDurations[(int)timeoutErrorPhase] == null)
			{
				this.phaseDurations[(int)timeoutErrorPhase] = new SqlConnectionTimeoutPhaseDuration();
			}
			this.phaseDurations[(int)timeoutErrorPhase].StartCapture();
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000C10F4 File Offset: 0x000C04F4
		internal void EndPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this.phaseDurations[(int)timeoutErrorPhase].StopCapture();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000C1110 File Offset: 0x000C0510
		internal void SetAllCompleteMarker()
		{
			this.currentPhase = SqlConnectionTimeoutErrorPhase.Complete;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000C1124 File Offset: 0x000C0524
		internal string GetErrorMessage()
		{
			StringBuilder stringBuilder;
			string text;
			switch (this.currentPhase)
			{
			case SqlConnectionTimeoutErrorPhase.PreLoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_Begin());
				text = SQLMessage.Duration_PreLogin_Begin(this.phaseDurations[1].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.InitializeConnection:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_InitializeConnection());
				text = SQLMessage.Duration_PreLogin_Begin(this.phaseDurations[1].GetMilliSecondDuration() + this.phaseDurations[2].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_SendHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this.phaseDurations[1].GetMilliSecondDuration() + this.phaseDurations[2].GetMilliSecondDuration(), this.phaseDurations[3].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_ConsumeHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this.phaseDurations[1].GetMilliSecondDuration() + this.phaseDurations[2].GetMilliSecondDuration(), this.phaseDurations[3].GetMilliSecondDuration() + this.phaseDurations[4].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.LoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_Begin());
				text = SQLMessage.Duration_Login_Begin(this.phaseDurations[1].GetMilliSecondDuration() + this.phaseDurations[2].GetMilliSecondDuration(), this.phaseDurations[3].GetMilliSecondDuration() + this.phaseDurations[4].GetMilliSecondDuration(), this.phaseDurations[5].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_ProcessConnectionAuth());
				text = SQLMessage.Duration_Login_ProcessConnectionAuth(this.phaseDurations[1].GetMilliSecondDuration() + this.phaseDurations[2].GetMilliSecondDuration(), this.phaseDurations[3].GetMilliSecondDuration() + this.phaseDurations[4].GetMilliSecondDuration(), this.phaseDurations[5].GetMilliSecondDuration(), this.phaseDurations[6].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.PostLogin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PostLogin());
				text = SQLMessage.Duration_PostLogin(this.phaseDurations[1].GetMilliSecondDuration() + this.phaseDurations[2].GetMilliSecondDuration(), this.phaseDurations[3].GetMilliSecondDuration() + this.phaseDurations[4].GetMilliSecondDuration(), this.phaseDurations[5].GetMilliSecondDuration(), this.phaseDurations[6].GetMilliSecondDuration(), this.phaseDurations[7].GetMilliSecondDuration());
				break;
			default:
				stringBuilder = new StringBuilder(SQLMessage.Timeout());
				text = null;
				break;
			}
			if (this.currentPhase != SqlConnectionTimeoutErrorPhase.Undefined && this.currentPhase != SqlConnectionTimeoutErrorPhase.Complete)
			{
				if (this.isFailoverScenario)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_FailoverInfo(), new object[]
					{
						this.currentSourceType
					});
				}
				else if (this.currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_RoutingDestination(), new object[]
					{
						this.originalPhaseDurations[1].GetMilliSecondDuration() + this.originalPhaseDurations[2].GetMilliSecondDuration(),
						this.originalPhaseDurations[3].GetMilliSecondDuration() + this.originalPhaseDurations[4].GetMilliSecondDuration(),
						this.originalPhaseDurations[5].GetMilliSecondDuration(),
						this.originalPhaseDurations[6].GetMilliSecondDuration(),
						this.originalPhaseDurations[7].GetMilliSecondDuration()
					});
				}
			}
			if (text != null)
			{
				stringBuilder.Append("  ");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001001 RID: 4097
		private SqlConnectionTimeoutPhaseDuration[] phaseDurations;

		// Token: 0x04001002 RID: 4098
		private SqlConnectionTimeoutPhaseDuration[] originalPhaseDurations;

		// Token: 0x04001003 RID: 4099
		private SqlConnectionTimeoutErrorPhase currentPhase;

		// Token: 0x04001004 RID: 4100
		private SqlConnectionInternalSourceType currentSourceType;

		// Token: 0x04001005 RID: 4101
		private bool isFailoverScenario;
	}
}
