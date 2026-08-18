using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x02000676 RID: 1654
	[ComVisible(true)]
	[Serializable]
	public sealed class NamedPermissionSet : PermissionSet
	{
		// Token: 0x06003BCF RID: 15311 RVA: 0x000CC256 File Offset: 0x000CB256
		internal NamedPermissionSet()
		{
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000CC25E File Offset: 0x000CB25E
		public NamedPermissionSet(string name)
		{
			NamedPermissionSet.CheckName(name);
			this.m_name = name;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000CC273 File Offset: 0x000CB273
		public NamedPermissionSet(string name, PermissionState state) : base(state)
		{
			NamedPermissionSet.CheckName(name);
			this.m_name = name;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000CC289 File Offset: 0x000CB289
		public NamedPermissionSet(string name, PermissionSet permSet) : base(permSet)
		{
			NamedPermissionSet.CheckName(name);
			this.m_name = name;
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000CC29F File Offset: 0x000CB29F
		public NamedPermissionSet(NamedPermissionSet permSet) : base(permSet)
		{
			this.m_name = permSet.m_name;
			this.m_description = permSet.Description;
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06003BD4 RID: 15316 RVA: 0x000CC2C0 File Offset: 0x000CB2C0
		// (set) Token: 0x06003BD5 RID: 15317 RVA: 0x000CC2C8 File Offset: 0x000CB2C8
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				NamedPermissionSet.CheckName(value);
				this.m_name = value;
			}
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000CC2D7 File Offset: 0x000CB2D7
		private static void CheckName(string name)
		{
			if (name == null || name.Equals(""))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"));
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000CC2F9 File Offset: 0x000CB2F9
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x000CC321 File Offset: 0x000CB321
		public string Description
		{
			get
			{
				if (this.m_descrResource != null)
				{
					this.m_description = Environment.GetResourceString(this.m_descrResource);
					this.m_descrResource = null;
				}
				return this.m_description;
			}
			set
			{
				this.m_description = value;
				this.m_descrResource = null;
			}
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000CC331 File Offset: 0x000CB331
		public override PermissionSet Copy()
		{
			return new NamedPermissionSet(this);
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000CC33C File Offset: 0x000CB33C
		public NamedPermissionSet Copy(string name)
		{
			return new NamedPermissionSet(this)
			{
				Name = name
			};
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x000CC358 File Offset: 0x000CB358
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.ToXml("System.Security.NamedPermissionSet");
			if (this.m_name != null && !this.m_name.Equals(""))
			{
				securityElement.AddAttribute("Name", SecurityElement.Escape(this.m_name));
			}
			if (this.Description != null && !this.Description.Equals(""))
			{
				securityElement.AddAttribute("Description", SecurityElement.Escape(this.Description));
			}
			return securityElement;
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000CC3D2 File Offset: 0x000CB3D2
		public override void FromXml(SecurityElement et)
		{
			this.FromXml(et, false, false);
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000CC3E0 File Offset: 0x000CB3E0
		internal override void FromXml(SecurityElement et, bool allowInternalOnly, bool ignoreTypeLoadFailures)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			string text = et.Attribute("Name");
			this.m_name = ((text == null) ? null : text);
			text = et.Attribute("Description");
			this.m_description = ((text == null) ? "" : text);
			this.m_descrResource = null;
			base.FromXml(et, allowInternalOnly, ignoreTypeLoadFailures);
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000CC444 File Offset: 0x000CB444
		internal void FromXmlNameOnly(SecurityElement et)
		{
			string text = et.Attribute("Name");
			this.m_name = ((text == null) ? null : text);
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000CC46A File Offset: 0x000CB46A
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000CC473 File Offset: 0x000CB473
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001EDB RID: 7899
		private string m_name;

		// Token: 0x04001EDC RID: 7900
		private string m_description;

		// Token: 0x04001EDD RID: 7901
		[OptionalField(VersionAdded = 2)]
		internal string m_descrResource;
	}
}
