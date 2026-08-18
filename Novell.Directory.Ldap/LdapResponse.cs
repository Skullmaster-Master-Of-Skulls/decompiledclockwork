using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000033 RID: 51
	public class LdapResponse : LdapMessage
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B334 File Offset: 0x0000A334
		public virtual string ErrorMessage
		{
			get
			{
				string result;
				if (this.exception != null)
				{
					result = this.exception.LdapErrorMessage;
				}
				else
				{
					result = ((RfcResponse)this.message.Response).getErrorMessage().stringValue();
				}
				return result;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000B378 File Offset: 0x0000A378
		public virtual string MatchedDN
		{
			get
			{
				string result;
				if (this.exception != null)
				{
					result = this.exception.MatchedDN;
				}
				else
				{
					result = ((RfcResponse)this.message.Response).getMatchedDN().stringValue();
				}
				return result;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B3BC File Offset: 0x0000A3BC
		public virtual string[] Referrals
		{
			get
			{
				string[] array = null;
				RfcReferral referral = ((RfcResponse)this.message.Response).getReferral();
				if (referral == null)
				{
					array = new string[0];
				}
				else
				{
					int num = referral.size();
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						string text = ((Asn1OctetString)referral.get_Renamed(i)).stringValue();
						try
						{
							LdapUrl ldapUrl = new LdapUrl(text);
							if (ldapUrl.getDN() == null)
							{
								RfcLdapMessage asn1Object = base.Asn1Object.RequestingMessage.Asn1Object;
								string requestDN;
								if ((requestDN = asn1Object.RequestDN) != null)
								{
									ldapUrl.setDN(requestDN);
									text = ldapUrl.ToString();
								}
							}
						}
						catch (UriFormatException ex)
						{
						}
						finally
						{
							array[i] = text;
						}
					}
				}
				return array;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000B4A8 File Offset: 0x0000A4A8
		public virtual int ResultCode
		{
			get
			{
				int result;
				if (this.exception != null)
				{
					result = this.exception.ResultCode;
				}
				else if (((RfcResponse)this.message.Response) is RfcIntermediateResponse)
				{
					result = 0;
				}
				else
				{
					result = ((RfcResponse)this.message.Response).getResultCode().intValue();
				}
				return result;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000B504 File Offset: 0x0000A504
		internal virtual LdapException ResultException
		{
			get
			{
				LdapException ex = null;
				int resultCode = this.ResultCode;
				if (resultCode != 0)
				{
					switch (resultCode)
					{
					case 5:
					case 6:
						break;
					default:
						if (resultCode != 10)
						{
							ex = new LdapException(LdapException.resultCodeToString(this.ResultCode), this.ResultCode, this.ErrorMessage, this.MatchedDN);
						}
						else
						{
							string[] referrals = this.Referrals;
							ex = new LdapReferralException("Automatic referral following not enabled", 10, this.ErrorMessage);
							((LdapReferralException)ex).setReferrals(referrals);
						}
						break;
					}
				}
				return ex;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000B58C File Offset: 0x0000A58C
		public override LdapControl[] Controls
		{
			get
			{
				LdapControl[] result;
				if (this.exception != null)
				{
					result = null;
				}
				else
				{
					result = base.Controls;
				}
				return result;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B5B0 File Offset: 0x0000A5B0
		public override int MessageID
		{
			get
			{
				int messageID;
				if (this.exception != null)
				{
					messageID = this.exception.MessageID;
				}
				else
				{
					messageID = base.MessageID;
				}
				return messageID;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000B5E0 File Offset: 0x0000A5E0
		public override int Type
		{
			get
			{
				int result;
				if (this.exception != null)
				{
					result = this.exception.ReplyType;
				}
				else
				{
					result = base.Type;
				}
				return result;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000B610 File Offset: 0x0000A610
		internal virtual LdapException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000B628 File Offset: 0x0000A628
		internal virtual ReferralInfo ActiveReferral
		{
			get
			{
				return this.activeReferral;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B640 File Offset: 0x0000A640
		public LdapResponse(InterThreadException ex, ReferralInfo activeReferral)
		{
			this.exception = ex;
			this.activeReferral = activeReferral;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000B66C File Offset: 0x0000A66C
		internal LdapResponse(RfcLdapMessage message) : base(message)
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000B68C File Offset: 0x0000A68C
		public LdapResponse(int type) : this(type, 0, null, null, null, null)
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000B6A8 File Offset: 0x0000A6A8
		public LdapResponse(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals, LdapControl[] controls) : base(new RfcLdapMessage(LdapResponse.RfcResultFactory(type, resultCode, matchedDN, serverMessage, referrals)))
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B6D8 File Offset: 0x0000A6D8
		private static Asn1Sequence RfcResultFactory(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals)
		{
			if (matchedDN == null)
			{
				matchedDN = "";
			}
			if (serverMessage == null)
			{
				serverMessage = "";
			}
			switch (type)
			{
			case 1:
				return null;
			case 2:
			case 3:
			case 6:
			case 8:
			case 10:
			case 12:
			case 14:
				break;
			case 4:
				return null;
			case 5:
				return new RfcSearchResultDone(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 7:
				return new RfcModifyResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 9:
				return new RfcAddResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 11:
				return new RfcDelResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 13:
				return new RfcModifyDNResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 15:
				return new RfcCompareResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			default:
				if (type == 19)
				{
					return null;
				}
				if (type == 24)
				{
					return null;
				}
				break;
			}
			throw new SystemException("Type " + type + " Not Supported");
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B834 File Offset: 0x0000A834
		internal virtual void chkResultCode()
		{
			if (this.exception != null)
			{
				throw this.exception;
			}
			LdapException resultException = this.ResultException;
			if (resultException != null)
			{
				throw resultException;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B860 File Offset: 0x0000A860
		internal virtual bool hasException()
		{
			return this.exception != null;
		}

		// Token: 0x0400010B RID: 267
		private InterThreadException exception = null;

		// Token: 0x0400010C RID: 268
		private ReferralInfo activeReferral;
	}
}
