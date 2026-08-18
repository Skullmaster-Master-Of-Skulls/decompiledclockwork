using System;
using System.Collections;
using System.Text;
using a.i;
using MailBee.ImapMail;
using MailBee.Mime;

namespace a.f
{
	// Token: 0x0200008A RID: 138
	internal class q : n
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000CFEF File Offset: 0x0000BFEF
		public static q a()
		{
			if (q.a == null)
			{
				q.a = new q();
			}
			return q.a;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000D007 File Offset: 0x0000C007
		public override int j9(string A_0, object A_1)
		{
			return 1;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000D00C File Offset: 0x0000C00C
		public override object ka(string A_0, object A_1, Encoding A_2)
		{
			uint num = global::b.a(A_0);
			if (num > 2237933493U)
			{
				if (num <= 2731641654U)
				{
					if (num != 2316323855U)
					{
						if (num != 2697828621U)
						{
							if (num != 2731641654U)
							{
								return A_1;
							}
							if (!(A_0 == "RFC822"))
							{
								return A_1;
							}
						}
						else
						{
							if (!(A_0 == "RFC822.HEADER"))
							{
								return A_1;
							}
							return A_1;
						}
					}
					else
					{
						if (!(A_0 == "RFC822.TEXT"))
						{
							return A_1;
						}
						return A_1;
					}
				}
				else if (num <= 3590698946U)
				{
					if (num != 3084371924U)
					{
						if (num != 3590698946U)
						{
							return A_1;
						}
						if (!(A_0 == "X-GM-LABELS"))
						{
							return A_1;
						}
						return ao.a(A_1 as ArrayList, A_2);
					}
					else
					{
						if (!(A_0 == "INTERNALDATE"))
						{
							return A_1;
						}
						try
						{
							return ImapUtils.GetDateTimeFromImapDate(((ao)A_1).a(A_2));
						}
						catch
						{
							return DateTime.MinValue;
						}
					}
				}
				else if (num != 3751961261U)
				{
					if (num != 3935480516U)
					{
						return A_1;
					}
					if (!(A_0 == "X-GM-THRID"))
					{
						return A_1;
					}
					return ((ao)A_1).a(Encoding.ASCII);
				}
				else
				{
					if (!(A_0 == "UID"))
					{
						return A_1;
					}
					try
					{
						return long.Parse(((ao)A_1).a(Encoding.ASCII));
					}
					catch
					{
						return -1;
					}
					goto IL_27C;
				}
				return A_1;
			}
			if (num <= 1069254540U)
			{
				if (num != 120742934U)
				{
					if (num != 617890069U)
					{
						if (num != 1069254540U)
						{
							return A_1;
						}
						if (!(A_0 == "FLAGS"))
						{
							return A_1;
						}
						return MessageFlagSet.a(A_1 as ArrayList, A_2);
					}
					else
					{
						if (!(A_0 == "ENVELOPE"))
						{
							return A_1;
						}
						return A_1;
					}
				}
				else
				{
					if (!(A_0 == "BODYSTRUCTURE"))
					{
						return A_1;
					}
					return ImapBodyStructure.b(A_1 as ArrayList, A_2);
				}
			}
			else if (num != 1110999001U)
			{
				if (num != 1598409751U)
				{
					if (num != 2237933493U)
					{
						return A_1;
					}
					if (!(A_0 == "BODY"))
					{
						return A_1;
					}
					return A_1;
				}
				else
				{
					if (!(A_0 == "X-GM-MSGID"))
					{
						return A_1;
					}
					goto IL_27C;
				}
			}
			else
			{
				if (!(A_0 == "RFC822.SIZE"))
				{
					return A_1;
				}
				try
				{
					return int.Parse(((ao)A_1).a(Encoding.ASCII));
				}
				catch
				{
					return -1;
				}
			}
			return A_1;
			IL_27C:
			return ((ao)A_1).a(Encoding.ASCII);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000D2F0 File Offset: 0x0000C2F0
		public static Envelope a(Hashtable A_0, int A_1, EnvelopeParts A_2, string A_3, string A_4, string A_5, string[] A_6, Encoding A_7)
		{
			bool flag = true;
			Encoding encoding = A_7;
			string text = null;
			string text2 = null;
			string[] a_ = null;
			MessageFlagSet messageFlagSet;
			if ((A_2 & EnvelopeParts.Flags) > EnvelopeParts.Uid)
			{
				messageFlagSet = (MessageFlagSet)A_0["FLAGS"];
				if (messageFlagSet == null)
				{
					flag = false;
				}
			}
			else
			{
				messageFlagSet = null;
			}
			int num;
			if ((A_2 & EnvelopeParts.Rfc822Size) > EnvelopeParts.Uid)
			{
				object obj = A_0["RFC822.SIZE"];
				if (obj == null)
				{
					num = -1;
				}
				else
				{
					num = (int)obj;
				}
				if (num < 0)
				{
					flag = false;
				}
			}
			else
			{
				num = -1;
			}
			DateTime dateTime;
			if ((A_2 & EnvelopeParts.InternalDate) > EnvelopeParts.Uid)
			{
				object obj2 = A_0["INTERNALDATE"];
				if (obj2 == null)
				{
					dateTime = DateTime.MinValue;
				}
				else
				{
					dateTime = (DateTime)obj2;
				}
				if (dateTime == DateTime.MinValue)
				{
					flag = false;
				}
			}
			else
			{
				dateTime = DateTime.MinValue;
			}
			object obj3 = A_0["UID"];
			long num2;
			if (obj3 == null)
			{
				num2 = -1L;
			}
			else
			{
				num2 = (long)obj3;
			}
			if (num2 < 0L)
			{
				flag = false;
			}
			if ((A_2 & EnvelopeParts.GmailMessageID) > EnvelopeParts.Uid)
			{
				text = (A_0["X-GM-MSGID"] as string);
				if (text == null)
				{
					flag = false;
				}
			}
			if ((A_2 & EnvelopeParts.GmailThreadID) > EnvelopeParts.Uid)
			{
				text2 = (A_0["X-GM-THRID"] as string);
				if (text2 == null)
				{
					flag = false;
				}
			}
			if ((A_2 & EnvelopeParts.GmailLabels) > EnvelopeParts.Uid)
			{
				a_ = (A_0["X-GM-LABELS"] as string[]);
				if (text2 == null)
				{
					flag = false;
				}
			}
			ImapBodyStructure imapBodyStructure = null;
			if ((A_2 & EnvelopeParts.BodyStructure) > EnvelopeParts.Uid)
			{
				imapBodyStructure = (A_0["BODYSTRUCTURE"] as ImapBodyStructure);
				if (imapBodyStructure == null)
				{
					imapBodyStructure = (A_0["BODY"] as ImapBodyStructure);
				}
				if (imapBodyStructure == null)
				{
					flag = false;
				}
				else
				{
					encoding = imapBodyStructure.CharsetEncoding;
				}
			}
			MailMessage mailMessage = null;
			if (A_3 != null)
			{
				A_3 = A_3.ToUpper();
				ao ao = A_0[A_3] as ao;
				if (ao == null)
				{
					flag = false;
				}
				else
				{
					byte[] array = null;
					if (A_4 != null)
					{
						A_4 = A_4.ToUpper();
						ao ao2 = A_0[A_4] as ao;
						if (ao2 != null)
						{
							array = ao2.c();
						}
					}
					if (array == null)
					{
						mailMessage = new MailMessage(ao);
					}
					else
					{
						byte[] array2 = ao.c();
						byte[] array3 = new byte[array2.Length + array.Length];
						Buffer.BlockCopy(array2, 0, array3, 0, array2.Length);
						Buffer.BlockCopy(array, 0, array3, array2.Length, array.Length);
						mailMessage = new MailMessage(array3);
					}
					if (mailMessage == null)
					{
						flag = false;
					}
					else
					{
						mailMessage.IndexOnServerInternal = A_1;
						if (num > -1)
						{
							mailMessage.b(num);
						}
						if (num2 > -1L)
						{
							mailMessage.UidOnServerInternal = num2;
						}
						if (imapBodyStructure == null && (A_2 & EnvelopeParts.Envelope) > EnvelopeParts.Uid)
						{
							try
							{
								int a_2 = k.a(ao.d(), ao.b(), ao.e());
								Header header = HeaderCollection.a(ao.a(A_7, ao.b(), a_2), null).a("content-type");
								if (header != null)
								{
									n n = header.HeaderParameters.b("charset");
									if (n != null)
									{
										string text3 = n.c();
										if (text3 != null)
										{
											encoding = Encoding.GetEncoding(text3);
										}
									}
								}
							}
							catch
							{
								flag = false;
							}
						}
					}
				}
			}
			if ((A_2 & EnvelopeParts.Envelope) > EnvelopeParts.Uid && (A_2 & EnvelopeParts.BodyStructure) == EnvelopeParts.Uid && (A_2 & EnvelopeParts.MessagePreview) == EnvelopeParts.Uid)
			{
				string text4 = null;
				try
				{
					text4 = ((ao)A_0["BODY[HEADER.FIELDS (CONTENT-TYPE)]"]).a(A_7);
				}
				catch
				{
				}
				if (text4 != null)
				{
					Header header2 = Header.a(text4.Trim());
					if (header2 != null && header2.HeaderParameters != null)
					{
						n n2 = header2.HeaderParameters.b("charset");
						if (n2 != null)
						{
							string text5 = n2.c();
							if (text5 != null)
							{
								try
								{
									encoding = Encoding.GetEncoding(text5);
								}
								catch
								{
								}
							}
						}
					}
				}
			}
			if (h.a(encoding))
			{
				encoding = A_7;
			}
			Envelope envelope = null;
			object obj4 = A_0["ENVELOPE"];
			if (obj4 != null)
			{
				envelope = Envelope.c(obj4 as ArrayList, encoding);
				A_0["ENVELOPE"] = envelope;
			}
			if ((A_2 & EnvelopeParts.Envelope) > EnvelopeParts.Uid)
			{
				if (envelope == null)
				{
					envelope = new Envelope();
					flag = false;
				}
				else if (flag)
				{
					flag = envelope.IsValid;
				}
			}
			else if (envelope == null)
			{
				envelope = new Envelope();
			}
			envelope.KeyValueList = A_0;
			envelope.a(encoding);
			envelope.a(A_1);
			envelope.a(messageFlagSet, dateTime, num, num2);
			if (imapBodyStructure != null)
			{
				envelope.a(imapBodyStructure);
			}
			if (mailMessage != null)
			{
				envelope.a(mailMessage);
			}
			if (A_5 != null)
			{
				A_5 = A_5.ToUpper();
				object obj5 = A_0[A_5];
				if (obj5 == null)
				{
					flag = false;
				}
				else
				{
					HeaderCollection headerCollection = null;
					try
					{
						headerCollection = HeaderCollection.a(((ao)obj5).a(encoding), null);
					}
					catch
					{
						flag = false;
					}
					if (headerCollection != null)
					{
						envelope.a(headerCollection);
					}
				}
			}
			envelope.a(text, text2, a_);
			envelope.a(flag);
			return envelope;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000D788 File Offset: 0x0000C788
		private static byte[] a(object A_0)
		{
			ao ao = A_0 as ao;
			if (ao == null)
			{
				return null;
			}
			return ao.c();
		}

		// Token: 0x04000225 RID: 549
		private static q a;
	}
}
