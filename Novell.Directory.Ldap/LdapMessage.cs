using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000019 RID: 25
	public class LdapMessage
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000592C File Offset: 0x0000492C
		internal virtual LdapMessage RequestingMessage
		{
			get
			{
				return this.message.RequestingMessage;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005948 File Offset: 0x00004948
		public virtual LdapControl[] Controls
		{
			get
			{
				LdapControl[] array = null;
				RfcControls controls = this.message.Controls;
				if (controls != null)
				{
					array = new LdapControl[controls.size()];
					for (int i = 0; i < controls.size(); i++)
					{
						RfcControl rfcControl = (RfcControl)controls.get_Renamed(i);
						string oid = rfcControl.ControlType.stringValue();
						sbyte[] value_Renamed = rfcControl.ControlValue.byteValue();
						bool critical = rfcControl.Criticality.booleanValue();
						array[i] = this.controlFactory(oid, critical, value_Renamed);
					}
				}
				return array;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000059D0 File Offset: 0x000049D0
		public virtual int MessageID
		{
			get
			{
				if (this.imsgNum == -1)
				{
					this.imsgNum = this.message.MessageID;
				}
				return this.imsgNum;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005A04 File Offset: 0x00004A04
		public virtual int Type
		{
			get
			{
				if (this.messageType == -1)
				{
					this.messageType = this.message.Type;
				}
				return this.messageType;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005A38 File Offset: 0x00004A38
		public virtual bool Request
		{
			get
			{
				return this.message.isRequest();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005A54 File Offset: 0x00004A54
		internal virtual RfcLdapMessage Asn1Object
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005A6C File Offset: 0x00004A6C
		private string Name
		{
			get
			{
				switch (this.Type)
				{
				case 0:
					return "LdapBindRequest";
				case 1:
					return "LdapBindResponse";
				case 2:
					return "LdapUnbindRequest";
				case 3:
					return "LdapSearchRequest";
				case 4:
					return "LdapSearchResponse";
				case 5:
					return "LdapSearchResult";
				case 6:
					return "LdapModifyRequest";
				case 7:
					return "LdapModifyResponse";
				case 8:
					return "LdapAddRequest";
				case 9:
					return "LdapAddResponse";
				case 10:
					return "LdapDelRequest";
				case 11:
					return "LdapDelResponse";
				case 12:
					return "LdapModifyRDNRequest";
				case 13:
					return "LdapModifyRDNResponse";
				case 14:
					return "LdapCompareRequest";
				case 15:
					return "LdapCompareResponse";
				case 16:
					return "LdapAbandonRequest";
				case 19:
					return "LdapSearchResultReference";
				case 23:
					return "LdapExtendedRequest";
				case 24:
					return "LdapExtendedResponse";
				case 25:
					return "LdapIntermediateResponse";
				}
				throw new SystemException("LdapMessage: Unknown Type " + this.Type);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005BD4 File Offset: 0x00004BD4
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005C18 File Offset: 0x00004C18
		public virtual string Tag
		{
			get
			{
				string result;
				if (this.stringTag != null)
				{
					result = this.stringTag;
				}
				else if (this.Request)
				{
					result = null;
				}
				else
				{
					LdapMessage requestingMessage = this.RequestingMessage;
					if (requestingMessage == null)
					{
						result = null;
					}
					else
					{
						result = requestingMessage.stringTag;
					}
				}
				return result;
			}
			set
			{
				this.stringTag = value;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005C30 File Offset: 0x00004C30
		internal LdapMessage()
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005C5C File Offset: 0x00004C5C
		internal LdapMessage(int type, RfcRequest op, LdapControl[] controls)
		{
			this.messageType = type;
			RfcControls rfcControls = null;
			if (controls != null)
			{
				rfcControls = new RfcControls();
				for (int i = 0; i < controls.Length; i++)
				{
					rfcControls.add(controls[i].Asn1Object);
				}
			}
			this.message = new RfcLdapMessage(op, rfcControls);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005CC4 File Offset: 0x00004CC4
		protected internal LdapMessage(RfcLdapMessage message)
		{
			this.message = message;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005CF8 File Offset: 0x00004CF8
		internal LdapMessage Clone(string dn, string filter, bool reference)
		{
			return new LdapMessage((RfcLdapMessage)this.message.dupMessage(dn, filter, reference));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005D24 File Offset: 0x00004D24
		private LdapControl controlFactory(string oid, bool critical, sbyte[] value_Renamed)
		{
			RespControlVector registeredControls = LdapControl.RegisteredControls;
			try
			{
				Type type = registeredControls.findResponseControl(oid);
				if (type == null)
				{
					return new LdapControl(oid, critical, value_Renamed);
				}
				Type[] types = new Type[]
				{
					typeof(string),
					typeof(bool),
					typeof(sbyte[])
				};
				object[] parameters = new object[]
				{
					oid,
					critical,
					value_Renamed
				};
				try
				{
					ConstructorInfo constructor = type.GetConstructor(types);
					try
					{
						object obj = constructor.Invoke(parameters);
						return (LdapControl)obj;
					}
					catch (UnauthorizedAccessException ex)
					{
					}
					catch (TargetInvocationException ex2)
					{
					}
					catch (Exception ex3)
					{
					}
				}
				catch (MethodAccessException ex4)
				{
				}
			}
			catch (FieldAccessException ex5)
			{
			}
			return new LdapControl(oid, critical, value_Renamed);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005E7C File Offset: 0x00004E7C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Name,
				"(",
				this.MessageID,
				"): ",
				this.message.ToString()
			});
		}

		// Token: 0x0400008C RID: 140
		public const int BIND_REQUEST = 0;

		// Token: 0x0400008D RID: 141
		public const int BIND_RESPONSE = 1;

		// Token: 0x0400008E RID: 142
		public const int UNBIND_REQUEST = 2;

		// Token: 0x0400008F RID: 143
		public const int SEARCH_REQUEST = 3;

		// Token: 0x04000090 RID: 144
		public const int SEARCH_RESPONSE = 4;

		// Token: 0x04000091 RID: 145
		public const int SEARCH_RESULT = 5;

		// Token: 0x04000092 RID: 146
		public const int MODIFY_REQUEST = 6;

		// Token: 0x04000093 RID: 147
		public const int MODIFY_RESPONSE = 7;

		// Token: 0x04000094 RID: 148
		public const int ADD_REQUEST = 8;

		// Token: 0x04000095 RID: 149
		public const int ADD_RESPONSE = 9;

		// Token: 0x04000096 RID: 150
		public const int DEL_REQUEST = 10;

		// Token: 0x04000097 RID: 151
		public const int DEL_RESPONSE = 11;

		// Token: 0x04000098 RID: 152
		public const int MODIFY_RDN_REQUEST = 12;

		// Token: 0x04000099 RID: 153
		public const int MODIFY_RDN_RESPONSE = 13;

		// Token: 0x0400009A RID: 154
		public const int COMPARE_REQUEST = 14;

		// Token: 0x0400009B RID: 155
		public const int COMPARE_RESPONSE = 15;

		// Token: 0x0400009C RID: 156
		public const int ABANDON_REQUEST = 16;

		// Token: 0x0400009D RID: 157
		public const int SEARCH_RESULT_REFERENCE = 19;

		// Token: 0x0400009E RID: 158
		public const int EXTENDED_REQUEST = 23;

		// Token: 0x0400009F RID: 159
		public const int EXTENDED_RESPONSE = 24;

		// Token: 0x040000A0 RID: 160
		public const int INTERMEDIATE_RESPONSE = 25;

		// Token: 0x040000A1 RID: 161
		protected internal RfcLdapMessage message;

		// Token: 0x040000A2 RID: 162
		private int imsgNum = -1;

		// Token: 0x040000A3 RID: 163
		private int messageType = -1;

		// Token: 0x040000A4 RID: 164
		private string stringTag = null;
	}
}
