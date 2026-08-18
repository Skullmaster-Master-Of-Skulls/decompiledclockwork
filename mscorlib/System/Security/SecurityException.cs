using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
	// Token: 0x02000690 RID: 1680
	[ComVisible(true)]
	[Serializable]
	public class SecurityException : SystemException
	{
		// Token: 0x06003CAD RID: 15533 RVA: 0x000CF781 File Offset: 0x000CE781
		internal static string GetResString(string sResourceName)
		{
			PermissionSet.s_fullTrust.Assert();
			return Environment.GetResourceString(sResourceName);
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000CF794 File Offset: 0x000CE794
		internal static Exception MakeSecurityException(AssemblyName asmName, Evidence asmEvidence, PermissionSet granted, PermissionSet refused, RuntimeMethodHandle rmh, SecurityAction action, object demand, IPermission permThatFailed)
		{
			HostProtectionPermission hostProtectionPermission = permThatFailed as HostProtectionPermission;
			if (hostProtectionPermission != null)
			{
				return new HostProtectionException(SecurityException.GetResString("HostProtection_HostProtection"), HostProtectionPermission.protectedResources, hostProtectionPermission.Resources);
			}
			string message = "";
			MethodInfo method = null;
			try
			{
				if (granted == null && refused == null && demand == null)
				{
					message = SecurityException.GetResString("Security_NoAPTCA");
				}
				else if (demand != null && demand is IPermission)
				{
					message = string.Format(CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), new object[]
					{
						demand.GetType().AssemblyQualifiedName
					});
				}
				else if (permThatFailed != null)
				{
					message = string.Format(CultureInfo.InvariantCulture, SecurityException.GetResString("Security_Generic"), new object[]
					{
						permThatFailed.GetType().AssemblyQualifiedName
					});
				}
				else
				{
					message = SecurityException.GetResString("Security_GenericNoType");
				}
				method = SecurityRuntime.GetMethodInfo(rmh);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException)
				{
					throw;
				}
			}
			return new SecurityException(message, asmName, granted, refused, method, action, demand, permThatFailed, asmEvidence);
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000CF89C File Offset: 0x000CE89C
		private static byte[] ObjectToByteArray(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			byte[] result;
			try
			{
				binaryFormatter.Serialize(memoryStream, obj);
				byte[] array = memoryStream.ToArray();
				result = array;
			}
			catch (NotSupportedException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000CF8E4 File Offset: 0x000CE8E4
		private static object ByteArrayToObject(byte[] array)
		{
			if (array == null || array.Length == 0)
			{
				return null;
			}
			MemoryStream serializationStream = new MemoryStream(array);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(serializationStream);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000CF911 File Offset: 0x000CE911
		public SecurityException() : base(SecurityException.GetResString("Arg_SecurityException"))
		{
			base.SetErrorCode(-2146233078);
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000CF92E File Offset: 0x000CE92E
		public SecurityException(string message) : base(message)
		{
			base.SetErrorCode(-2146233078);
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x000CF942 File Offset: 0x000CE942
		public SecurityException(string message, Type type) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.m_typeOfPermissionThatFailed = type;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000CF967 File Offset: 0x000CE967
		public SecurityException(string message, Type type, string state) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.m_typeOfPermissionThatFailed = type;
			this.m_demanded = state;
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000CF993 File Offset: 0x000CE993
		public SecurityException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233078);
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000CF9A8 File Offset: 0x000CE9A8
		internal SecurityException(PermissionSet grantedSetObj, PermissionSet refusedSetObj) : base(SecurityException.GetResString("Arg_SecurityException"))
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			if (grantedSetObj != null)
			{
				this.m_granted = grantedSetObj.ToXml().ToString();
			}
			if (refusedSetObj != null)
			{
				this.m_refused = refusedSetObj.ToXml().ToString();
			}
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000CFA04 File Offset: 0x000CEA04
		internal SecurityException(string message, PermissionSet grantedSetObj, PermissionSet refusedSetObj) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			if (grantedSetObj != null)
			{
				this.m_granted = grantedSetObj.ToXml().ToString();
			}
			if (refusedSetObj != null)
			{
				this.m_refused = refusedSetObj.ToXml().ToString();
			}
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000CFA58 File Offset: 0x000CEA58
		protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			try
			{
				this.m_action = (SecurityAction)info.GetValue("Action", typeof(SecurityAction));
				this.m_permissionThatFailed = (string)info.GetValueNoThrow("FirstPermissionThatFailed", typeof(string));
				this.m_demanded = (string)info.GetValueNoThrow("Demanded", typeof(string));
				this.m_granted = (string)info.GetValueNoThrow("GrantedSet", typeof(string));
				this.m_refused = (string)info.GetValueNoThrow("RefusedSet", typeof(string));
				this.m_denied = (string)info.GetValueNoThrow("Denied", typeof(string));
				this.m_permitOnly = (string)info.GetValueNoThrow("PermitOnly", typeof(string));
				this.m_assemblyName = (AssemblyName)info.GetValueNoThrow("Assembly", typeof(AssemblyName));
				this.m_serializedMethodInfo = (byte[])info.GetValueNoThrow("Method", typeof(byte[]));
				this.m_strMethodInfo = (string)info.GetValueNoThrow("Method_String", typeof(string));
				this.m_zone = (SecurityZone)info.GetValue("Zone", typeof(SecurityZone));
				this.m_url = (string)info.GetValueNoThrow("Url", typeof(string));
			}
			catch
			{
				this.m_action = (SecurityAction)0;
				this.m_permissionThatFailed = "";
				this.m_demanded = "";
				this.m_granted = "";
				this.m_refused = "";
				this.m_denied = "";
				this.m_permitOnly = "";
				this.m_assemblyName = null;
				this.m_serializedMethodInfo = null;
				this.m_strMethodInfo = null;
				this.m_zone = SecurityZone.NoZone;
				this.m_url = "";
			}
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x000CFC8C File Offset: 0x000CEC8C
		public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.Action = action;
			if (permThatFailed != null)
			{
				this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
			}
			this.FirstPermissionThatFailed = permThatFailed;
			this.Demanded = demanded;
			this.m_granted = ((grant == null) ? "" : grant.ToXml().ToString());
			this.m_refused = ((refused == null) ? "" : refused.ToXml().ToString());
			this.m_denied = "";
			this.m_permitOnly = "";
			this.m_assemblyName = assemblyName;
			this.Method = method;
			this.m_url = "";
			this.m_zone = SecurityZone.NoZone;
			if (evidence != null)
			{
				Url url = (Url)evidence.FindType(typeof(Url));
				if (url != null)
				{
					this.m_url = url.GetURLString().ToString();
				}
				Zone zone = (Zone)evidence.FindType(typeof(Zone));
				if (zone != null)
				{
					this.m_zone = zone.SecurityZone;
				}
			}
			this.m_debugString = this.ToString(true, false);
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000CFDB0 File Offset: 0x000CEDB0
		public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed) : base(message)
		{
			PermissionSet.s_fullTrust.Assert();
			base.SetErrorCode(-2146233078);
			this.Action = SecurityAction.Demand;
			if (permThatFailed != null)
			{
				this.m_typeOfPermissionThatFailed = permThatFailed.GetType();
			}
			this.FirstPermissionThatFailed = permThatFailed;
			this.Demanded = demanded;
			this.m_granted = "";
			this.m_refused = "";
			this.DenySetInstance = deny;
			this.PermitOnlySetInstance = permitOnly;
			this.m_assemblyName = null;
			this.Method = method;
			this.m_zone = SecurityZone.NoZone;
			this.m_url = "";
			this.m_debugString = this.ToString(true, false);
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000CFE54 File Offset: 0x000CEE54
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x000CFE5C File Offset: 0x000CEE5C
		[ComVisible(false)]
		public SecurityAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000CFE68 File Offset: 0x000CEE68
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x000CFEAD File Offset: 0x000CEEAD
		public Type PermissionType
		{
			get
			{
				if (this.m_typeOfPermissionThatFailed == null)
				{
					object obj = XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed);
					if (obj == null)
					{
						obj = XMLUtil.XmlStringToSecurityObject(this.m_demanded);
					}
					if (obj != null)
					{
						this.m_typeOfPermissionThatFailed = obj.GetType();
					}
				}
				return this.m_typeOfPermissionThatFailed;
			}
			set
			{
				this.m_typeOfPermissionThatFailed = value;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000CFEB6 File Offset: 0x000CEEB6
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x000CFEC8 File Offset: 0x000CEEC8
		public IPermission FirstPermissionThatFailed
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return (IPermission)XMLUtil.XmlStringToSecurityObject(this.m_permissionThatFailed);
			}
			set
			{
				this.m_permissionThatFailed = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000CFED6 File Offset: 0x000CEED6
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x000CFEDE File Offset: 0x000CEEDE
		public string PermissionState
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_demanded;
			}
			set
			{
				this.m_demanded = value;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000CFEE7 File Offset: 0x000CEEE7
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x000CFEF4 File Offset: 0x000CEEF4
		[ComVisible(false)]
		public object Demanded
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return XMLUtil.XmlStringToSecurityObject(this.m_demanded);
			}
			set
			{
				this.m_demanded = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000CFF02 File Offset: 0x000CEF02
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x000CFF0A File Offset: 0x000CEF0A
		public string GrantedSet
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_granted;
			}
			set
			{
				this.m_granted = value;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000CFF13 File Offset: 0x000CEF13
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x000CFF1B File Offset: 0x000CEF1B
		public string RefusedSet
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_refused;
			}
			set
			{
				this.m_refused = value;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000CFF24 File Offset: 0x000CEF24
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x000CFF31 File Offset: 0x000CEF31
		[ComVisible(false)]
		public object DenySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return XMLUtil.XmlStringToSecurityObject(this.m_denied);
			}
			set
			{
				this.m_denied = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000CFF3F File Offset: 0x000CEF3F
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x000CFF4C File Offset: 0x000CEF4C
		[ComVisible(false)]
		public object PermitOnlySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return XMLUtil.XmlStringToSecurityObject(this.m_permitOnly);
			}
			set
			{
				this.m_permitOnly = XMLUtil.SecurityObjectToXmlString(value);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000CFF5A File Offset: 0x000CEF5A
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x000CFF62 File Offset: 0x000CEF62
		[ComVisible(false)]
		public AssemblyName FailedAssemblyInfo
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_assemblyName;
			}
			set
			{
				this.m_assemblyName = value;
			}
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000CFF6B File Offset: 0x000CEF6B
		private MethodInfo getMethod()
		{
			return (MethodInfo)SecurityException.ByteArrayToObject(this.m_serializedMethodInfo);
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x000CFF7D File Offset: 0x000CEF7D
		// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x000CFF88 File Offset: 0x000CEF88
		[ComVisible(false)]
		public MethodInfo Method
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.getMethod();
			}
			set
			{
				RuntimeMethodInfo runtimeMethodInfo = value as RuntimeMethodInfo;
				this.m_serializedMethodInfo = SecurityException.ObjectToByteArray(runtimeMethodInfo);
				if (runtimeMethodInfo != null)
				{
					this.m_strMethodInfo = runtimeMethodInfo.ToString();
				}
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000CFFB7 File Offset: 0x000CEFB7
		// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x000CFFBF File Offset: 0x000CEFBF
		public SecurityZone Zone
		{
			get
			{
				return this.m_zone;
			}
			set
			{
				this.m_zone = value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000CFFC8 File Offset: 0x000CEFC8
		// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x000CFFD0 File Offset: 0x000CEFD0
		public string Url
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000CFFDC File Offset: 0x000CEFDC
		private void ToStringHelper(StringBuilder sb, string resourceString, object attr)
		{
			if (attr == null)
			{
				return;
			}
			string text = attr as string;
			if (text == null)
			{
				text = attr.ToString();
			}
			if (text.Length == 0)
			{
				return;
			}
			sb.Append(Environment.NewLine);
			sb.Append(SecurityException.GetResString(resourceString));
			sb.Append(Environment.NewLine);
			sb.Append(text);
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000D0034 File Offset: 0x000CF034
		private string ToString(bool includeSensitiveInfo, bool includeBaseInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (includeBaseInfo)
			{
				try
				{
					stringBuilder.Append(base.ToString());
				}
				catch (SecurityException)
				{
				}
			}
			PermissionSet.s_fullTrust.Assert();
			if (this.Action > (SecurityAction)0)
			{
				this.ToStringHelper(stringBuilder, "Security_Action", this.Action);
			}
			this.ToStringHelper(stringBuilder, "Security_TypeFirstPermThatFailed", this.PermissionType);
			if (includeSensitiveInfo)
			{
				this.ToStringHelper(stringBuilder, "Security_FirstPermThatFailed", this.m_permissionThatFailed);
				this.ToStringHelper(stringBuilder, "Security_Demanded", this.m_demanded);
				this.ToStringHelper(stringBuilder, "Security_GrantedSet", this.m_granted);
				this.ToStringHelper(stringBuilder, "Security_RefusedSet", this.m_refused);
				this.ToStringHelper(stringBuilder, "Security_Denied", this.m_denied);
				this.ToStringHelper(stringBuilder, "Security_PermitOnly", this.m_permitOnly);
				this.ToStringHelper(stringBuilder, "Security_Assembly", this.m_assemblyName);
				this.ToStringHelper(stringBuilder, "Security_Method", this.m_strMethodInfo);
			}
			if (this.m_zone != SecurityZone.NoZone)
			{
				this.ToStringHelper(stringBuilder, "Security_Zone", this.m_zone);
			}
			if (includeSensitiveInfo)
			{
				this.ToStringHelper(stringBuilder, "Security_Url", this.m_url);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000D017C File Offset: 0x000CF17C
		private bool CanAccessSensitiveInfo()
		{
			bool result = false;
			try
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy).Demand();
				result = true;
			}
			catch (SecurityException)
			{
			}
			return result;
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x000D01B0 File Offset: 0x000CF1B0
		public override string ToString()
		{
			return this.ToString(this.CanAccessSensitiveInfo(), true);
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x000D01C0 File Offset: 0x000CF1C0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("Action", this.m_action, typeof(SecurityAction));
			info.AddValue("FirstPermissionThatFailed", this.m_permissionThatFailed, typeof(string));
			info.AddValue("Demanded", this.m_demanded, typeof(string));
			info.AddValue("GrantedSet", this.m_granted, typeof(string));
			info.AddValue("RefusedSet", this.m_refused, typeof(string));
			info.AddValue("Denied", this.m_denied, typeof(string));
			info.AddValue("PermitOnly", this.m_permitOnly, typeof(string));
			info.AddValue("Assembly", this.m_assemblyName, typeof(AssemblyName));
			info.AddValue("Method", this.m_serializedMethodInfo, typeof(byte[]));
			info.AddValue("Method_String", this.m_strMethodInfo, typeof(string));
			info.AddValue("Zone", this.m_zone, typeof(SecurityZone));
			info.AddValue("Url", this.m_url, typeof(string));
		}

		// Token: 0x04001F2A RID: 7978
		private const string ActionName = "Action";

		// Token: 0x04001F2B RID: 7979
		private const string FirstPermissionThatFailedName = "FirstPermissionThatFailed";

		// Token: 0x04001F2C RID: 7980
		private const string DemandedName = "Demanded";

		// Token: 0x04001F2D RID: 7981
		private const string GrantedSetName = "GrantedSet";

		// Token: 0x04001F2E RID: 7982
		private const string RefusedSetName = "RefusedSet";

		// Token: 0x04001F2F RID: 7983
		private const string DeniedName = "Denied";

		// Token: 0x04001F30 RID: 7984
		private const string PermitOnlyName = "PermitOnly";

		// Token: 0x04001F31 RID: 7985
		private const string Assembly_Name = "Assembly";

		// Token: 0x04001F32 RID: 7986
		private const string MethodName_Serialized = "Method";

		// Token: 0x04001F33 RID: 7987
		private const string MethodName_String = "Method_String";

		// Token: 0x04001F34 RID: 7988
		private const string ZoneName = "Zone";

		// Token: 0x04001F35 RID: 7989
		private const string UrlName = "Url";

		// Token: 0x04001F36 RID: 7990
		private string m_debugString;

		// Token: 0x04001F37 RID: 7991
		private SecurityAction m_action;

		// Token: 0x04001F38 RID: 7992
		[NonSerialized]
		private Type m_typeOfPermissionThatFailed;

		// Token: 0x04001F39 RID: 7993
		private string m_permissionThatFailed;

		// Token: 0x04001F3A RID: 7994
		private string m_demanded;

		// Token: 0x04001F3B RID: 7995
		private string m_granted;

		// Token: 0x04001F3C RID: 7996
		private string m_refused;

		// Token: 0x04001F3D RID: 7997
		private string m_denied;

		// Token: 0x04001F3E RID: 7998
		private string m_permitOnly;

		// Token: 0x04001F3F RID: 7999
		private AssemblyName m_assemblyName;

		// Token: 0x04001F40 RID: 8000
		private byte[] m_serializedMethodInfo;

		// Token: 0x04001F41 RID: 8001
		private string m_strMethodInfo;

		// Token: 0x04001F42 RID: 8002
		private SecurityZone m_zone;

		// Token: 0x04001F43 RID: 8003
		private string m_url;
	}
}
