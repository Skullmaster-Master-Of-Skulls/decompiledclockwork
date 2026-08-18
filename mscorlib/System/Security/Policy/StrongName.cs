using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x020004B7 RID: 1207
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : IIdentityPermissionFactory, IBuiltInEvidence, IDelayEvaluatedEvidence
	{
		// Token: 0x06003013 RID: 12307 RVA: 0x000A4D6F File Offset: 0x000A3D6F
		internal StrongName()
		{
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000A4D77 File Offset: 0x000A3D77
		public StrongName(StrongNamePublicKeyBlob blob, string name, Version version) : this(blob, name, version, null)
		{
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000A4D84 File Offset: 0x000A3D84
		internal StrongName(StrongNamePublicKeyBlob blob, string name, Version version, Assembly assembly)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
			}
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.m_publicKeyBlob = blob;
			this.m_name = name;
			this.m_version = version;
			this.m_assembly = assembly;
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003016 RID: 12310 RVA: 0x000A4DFC File Offset: 0x000A3DFC
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.m_publicKeyBlob;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000A4E04 File Offset: 0x000A3E04
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003018 RID: 12312 RVA: 0x000A4E0C File Offset: 0x000A3E0C
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000A4E14 File Offset: 0x000A3E14
		bool IDelayEvaluatedEvidence.IsVerified
		{
			get
			{
				return this.m_assembly == null || this.m_assembly.IsStrongNameVerified();
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600301A RID: 12314 RVA: 0x000A4E2B File Offset: 0x000A3E2B
		bool IDelayEvaluatedEvidence.WasUsed
		{
			get
			{
				return this.m_wasUsed;
			}
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000A4E33 File Offset: 0x000A3E33
		void IDelayEvaluatedEvidence.MarkUsed()
		{
			this.m_wasUsed = true;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000A4E3C File Offset: 0x000A3E3C
		internal static bool CompareNames(string asmName, string mcName)
		{
			if (mcName.Length > 0 && mcName[mcName.Length - 1] == '*' && mcName.Length - 1 <= asmName.Length)
			{
				return string.Compare(mcName, 0, asmName, 0, mcName.Length - 1, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(mcName, asmName, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000A4E95 File Offset: 0x000A3E95
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000A4EAE File Offset: 0x000A3EAE
		public object Copy()
		{
			return new StrongName(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000A4EC8 File Offset: 0x000A3EC8
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("StrongName");
			securityElement.AddAttribute("version", "1");
			if (this.m_publicKeyBlob != null)
			{
				securityElement.AddAttribute("Key", Hex.EncodeHexString(this.m_publicKeyBlob.PublicKey));
			}
			if (this.m_name != null)
			{
				securityElement.AddAttribute("Name", this.m_name);
			}
			if (this.m_version != null)
			{
				securityElement.AddAttribute("Version", this.m_version.ToString());
			}
			return securityElement;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000A4F54 File Offset: 0x000A3F54
		internal void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (string.Compare(element.Tag, "StrongName", StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
			}
			this.m_publicKeyBlob = null;
			this.m_version = null;
			string text = element.Attribute("Key");
			if (text != null)
			{
				this.m_publicKeyBlob = new StrongNamePublicKeyBlob(Hex.DecodeHexString(text));
			}
			this.m_name = element.Attribute("Name");
			string text2 = element.Attribute("Version");
			if (text2 != null)
			{
				this.m_version = new Version(text2);
			}
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000A4FEC File Offset: 0x000A3FEC
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000A4FFC File Offset: 0x000A3FFC
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && object.Equals(this.m_publicKeyBlob, strongName.m_publicKeyBlob) && object.Equals(this.m_name, strongName.m_name) && object.Equals(this.m_version, strongName.m_version);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000A504C File Offset: 0x000A404C
		public override int GetHashCode()
		{
			if (this.m_publicKeyBlob != null)
			{
				return this.m_publicKeyBlob.GetHashCode();
			}
			if (this.m_name != null || this.m_version != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_version == null) ? 0 : this.m_version.GetHashCode());
			}
			return typeof(StrongName).GetHashCode();
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000A50C8 File Offset: 0x000A40C8
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\u0002';
			int num = this.m_publicKeyBlob.PublicKey.Length;
			if (verbose)
			{
				BuiltInEvidenceHelper.CopyIntToCharArray(num, buffer, position);
				position += 2;
			}
			Buffer.InternalBlockCopy(this.m_publicKeyBlob.PublicKey, 0, buffer, position * 2, num);
			position += (num - 1) / 2 + 1;
			BuiltInEvidenceHelper.CopyIntToCharArray(this.m_version.Major, buffer, position);
			BuiltInEvidenceHelper.CopyIntToCharArray(this.m_version.Minor, buffer, position + 2);
			BuiltInEvidenceHelper.CopyIntToCharArray(this.m_version.Build, buffer, position + 4);
			BuiltInEvidenceHelper.CopyIntToCharArray(this.m_version.Revision, buffer, position + 6);
			position += 8;
			int length = this.m_name.Length;
			if (verbose)
			{
				BuiltInEvidenceHelper.CopyIntToCharArray(length, buffer, position);
				position += 2;
			}
			this.m_name.CopyTo(0, buffer, position, length);
			return length + position;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000A51A0 File Offset: 0x000A41A0
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			int num = (this.m_publicKeyBlob.PublicKey.Length - 1) / 2 + 1;
			if (verbose)
			{
				num += 2;
			}
			num += 8;
			num += this.m_name.Length;
			if (verbose)
			{
				num += 2;
			}
			return num + 1;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000A51E8 File Offset: 0x000A41E8
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			int intFromCharArray = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			position += 2;
			this.m_publicKeyBlob = new StrongNamePublicKeyBlob();
			this.m_publicKeyBlob.PublicKey = new byte[intFromCharArray];
			int num = (intFromCharArray - 1) / 2 + 1;
			Buffer.InternalBlockCopy(buffer, position * 2, this.m_publicKeyBlob.PublicKey, 0, intFromCharArray);
			position += num;
			int intFromCharArray2 = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			int intFromCharArray3 = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position + 2);
			int intFromCharArray4 = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position + 4);
			int intFromCharArray5 = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position + 6);
			this.m_version = new Version(intFromCharArray2, intFromCharArray3, intFromCharArray4, intFromCharArray5);
			position += 8;
			intFromCharArray = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			position += 2;
			this.m_name = new string(buffer, position, intFromCharArray);
			return position + intFromCharArray;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000A52A0 File Offset: 0x000A42A0
		internal object Normalize()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.m_publicKeyBlob.PublicKey);
			binaryWriter.Write(this.m_version.Major);
			binaryWriter.Write(this.m_name);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x0400185B RID: 6235
		private StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x0400185C RID: 6236
		private string m_name;

		// Token: 0x0400185D RID: 6237
		private Version m_version;

		// Token: 0x0400185E RID: 6238
		[NonSerialized]
		private Assembly m_assembly;

		// Token: 0x0400185F RID: 6239
		[NonSerialized]
		private bool m_wasUsed;
	}
}
