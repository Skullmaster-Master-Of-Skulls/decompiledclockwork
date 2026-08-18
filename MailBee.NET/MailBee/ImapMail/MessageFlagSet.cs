using System;
using System.Collections;
using System.Text;
using a;
using a.f;

namespace MailBee.ImapMail
{
	// Token: 0x02000196 RID: 406
	public class MessageFlagSet
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x0003626A File Offset: 0x0003526A
		internal MessageFlagSet()
		{
			this.c = new string[0];
			this.a = false;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00036285 File Offset: 0x00035285
		private MessageFlagSet(string[] A_0)
		{
			this.c = A_0;
			this.a = false;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0003629B File Offset: 0x0003529B
		public SystemMessageFlags SystemFlags
		{
			get
			{
				if (!this.a)
				{
					this.b = this.a(this.c);
					this.a = true;
				}
				return this.b;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x000362C4 File Offset: 0x000352C4
		public string[] AllFlags
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000362CC File Offset: 0x000352CC
		public override string ToString()
		{
			return string.Join(" ", this.c);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000362E0 File Offset: 0x000352E0
		internal static MessageFlagSet a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			string[] array = ao.a(A_0, A_1);
			if (array == null)
			{
				return null;
			}
			return new MessageFlagSet(array);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00036305 File Offset: 0x00035305
		public static string SystemFlagsToString(SystemMessageFlags systemFlags)
		{
			return global::a.f.b.a(systemFlags);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00036310 File Offset: 0x00035310
		private SystemMessageFlags a(string[] A_0)
		{
			SystemMessageFlags systemMessageFlags = SystemMessageFlags.None;
			foreach (string text in A_0)
			{
				if (text != null)
				{
					text = text.ToUpper();
					uint num = global::b.a(text);
					if (num <= 2222891316U)
					{
						if (num != 290545591U)
						{
							if (num != 1250918916U)
							{
								if (num == 2222891316U)
								{
									if (text == "\\ANSWERED")
									{
										systemMessageFlags |= SystemMessageFlags.Answered;
										goto IL_117;
									}
								}
							}
							else if (text == "\\DELETED")
							{
								systemMessageFlags |= SystemMessageFlags.Deleted;
								goto IL_117;
							}
						}
						else if (text == "\\FLAGGED")
						{
							systemMessageFlags |= SystemMessageFlags.Flagged;
							goto IL_117;
						}
					}
					else if (num <= 2668212323U)
					{
						if (num != 2553797384U)
						{
							if (num == 2668212323U)
							{
								if (text == "\\*")
								{
									systemMessageFlags |= SystemMessageFlags.CanCreate;
									goto IL_117;
								}
							}
						}
						else if (text == "\\RECENT")
						{
							systemMessageFlags |= SystemMessageFlags.Recent;
							goto IL_117;
						}
					}
					else if (num != 2958173334U)
					{
						if (num == 3866871000U)
						{
							if (text == "\\SEEN")
							{
								systemMessageFlags |= SystemMessageFlags.Seen;
								goto IL_117;
							}
						}
					}
					else if (text == "\\DRAFT")
					{
						systemMessageFlags |= SystemMessageFlags.Draft;
						goto IL_117;
					}
					systemMessageFlags |= SystemMessageFlags.Other;
				}
				IL_117:;
			}
			return systemMessageFlags;
		}

		// Token: 0x0400094C RID: 2380
		private bool a;

		// Token: 0x0400094D RID: 2381
		private SystemMessageFlags b;

		// Token: 0x0400094E RID: 2382
		private string[] c;
	}
}
