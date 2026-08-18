using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x02000134 RID: 308
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class DBDataPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x002401A8 File Offset: 0x0023F5A8
		[Obsolete("DBDataPermission() has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission() : this(PermissionState.None)
		{
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x002401C8 File Offset: 0x0023F5C8
		protected DBDataPermission(PermissionState state)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (state == PermissionState.Unrestricted)
			{
				this._isUnrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this._isUnrestricted = false;
				return;
			}
			throw ADP.InvalidPermissionState(state);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00240208 File Offset: 0x0023F608
		[Obsolete("DBDataPermission(PermissionState state,Boolean allowBlankPassword) has been deprecated.  Use the DBDataPermission(PermissionState.None) constructor.  http://go.microsoft.com/fwlink/?linkid=14202", true)]
		protected DBDataPermission(PermissionState state, bool allowBlankPassword) : this(state)
		{
			this.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00240228 File Offset: 0x0023F628
		protected DBDataPermission(DBDataPermission permission)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permission == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this.CopyFrom(permission);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00240268 File Offset: 0x0023F668
		protected DBDataPermission(DBDataPermissionAttribute permissionAttribute)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (permissionAttribute == null)
			{
				throw ADP.ArgumentNull("permissionAttribute");
			}
			this._isUnrestricted = permissionAttribute.Unrestricted;
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permissionAttribute.AllowBlankPassword;
				if (permissionAttribute.ShouldSerializeConnectionString() || permissionAttribute.ShouldSerializeKeyRestrictions())
				{
					this.Add(permissionAttribute.ConnectionString, permissionAttribute.KeyRestrictions, permissionAttribute.KeyRestrictionBehavior);
				}
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x002402E8 File Offset: 0x0023F6E8
		internal DBDataPermission(DbConnectionOptions connectionOptions)
		{
			this._keyvaluetree = NameValuePermission.Default;
			base..ctor();
			if (connectionOptions != null)
			{
				this._allowBlankPassword = connectionOptions.HasBlankPassword;
				this.AddPermissionEntry(new DBConnectionString(connectionOptions));
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00240328 File Offset: 0x0023F728
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x00240348 File Offset: 0x0023F748
		public bool AllowBlankPassword
		{
			get
			{
				return this._allowBlankPassword;
			}
			set
			{
				this._allowBlankPassword = value;
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00240368 File Offset: 0x0023F768
		public virtual void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			DBConnectionString entry = new DBConnectionString(connectionString, restrictions, behavior, null, false);
			this.AddPermissionEntry(entry);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00240388 File Offset: 0x0023F788
		internal void AddPermissionEntry(DBConnectionString entry)
		{
			if (this._keyvaluetree == null)
			{
				this._keyvaluetree = new NameValuePermission();
			}
			if (this._keyvalues == null)
			{
				this._keyvalues = new ArrayList();
			}
			NameValuePermission.AddEntry(this._keyvaluetree, this._keyvalues, entry);
			this._isUnrestricted = false;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x002403D8 File Offset: 0x0023F7D8
		protected void Clear()
		{
			this._keyvaluetree = null;
			this._keyvalues = null;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x002403F8 File Offset: 0x0023F7F8
		public override IPermission Copy()
		{
			DBDataPermission dbdataPermission = this.CreateInstance();
			dbdataPermission.CopyFrom(this);
			return dbdataPermission;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00240418 File Offset: 0x0023F818
		private void CopyFrom(DBDataPermission permission)
		{
			this._isUnrestricted = permission.IsUnrestricted();
			if (!this._isUnrestricted)
			{
				this._allowBlankPassword = permission.AllowBlankPassword;
				if (permission._keyvalues != null)
				{
					this._keyvalues = (ArrayList)permission._keyvalues.Clone();
					if (permission._keyvaluetree != null)
					{
						this._keyvaluetree = permission._keyvaluetree.CopyNameValue();
					}
				}
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00240488 File Offset: 0x0023F888
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected virtual DBDataPermission CreateInstance()
		{
			return Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null) as DBDataPermission;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x002404B8 File Offset: 0x0023F8B8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target;
			if (dbdataPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission2 = (DBDataPermission)dbdataPermission.Copy();
			dbdataPermission2._allowBlankPassword &= this.AllowBlankPassword;
			if (this._keyvalues != null && dbdataPermission2._keyvalues != null)
			{
				dbdataPermission2._keyvalues.Clear();
				dbdataPermission2._keyvaluetree.Intersect(dbdataPermission2._keyvalues, this._keyvaluetree);
			}
			else
			{
				dbdataPermission2._keyvalues = null;
				dbdataPermission2._keyvaluetree = null;
			}
			if (dbdataPermission2.IsEmpty())
			{
				dbdataPermission2 = null;
			}
			return dbdataPermission2;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00240578 File Offset: 0x0023F978
		private bool IsEmpty()
		{
			ArrayList keyvalues = this._keyvalues;
			return !this.IsUnrestricted() && !this.AllowBlankPassword && (keyvalues == null || 0 == keyvalues.Count);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x002405B8 File Offset: 0x0023F9B8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			DBDataPermission dbdataPermission = target as DBDataPermission;
			bool flag = dbdataPermission.IsUnrestricted();
			if (!flag && !this.IsUnrestricted() && (!this.AllowBlankPassword || dbdataPermission.AllowBlankPassword) && (this._keyvalues == null || dbdataPermission._keyvaluetree != null))
			{
				flag = true;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString parsetable = (DBConnectionString)obj;
						if (!dbdataPermission._keyvaluetree.CheckValueForKeyPermit(parsetable))
						{
							flag = false;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00240698 File Offset: 0x0023FA98
		public bool IsUnrestricted()
		{
			return this._isUnrestricted;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x002406B8 File Offset: 0x0023FAB8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != base.GetType())
			{
				throw ADP.PermissionTypeMismatch();
			}
			if (this.IsUnrestricted())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission = (DBDataPermission)target.Copy();
			if (!dbdataPermission.IsUnrestricted())
			{
				dbdataPermission._allowBlankPassword |= this.AllowBlankPassword;
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString entry = (DBConnectionString)obj;
						dbdataPermission.AddPermissionEntry(entry);
					}
				}
			}
			if (!dbdataPermission.IsEmpty())
			{
				return dbdataPermission;
			}
			return null;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00240788 File Offset: 0x0023FB88
		private string DecodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace("&quot;", "\"");
				value = value.Replace("&apos;", "'");
				value = value.Replace("&lt;", "<");
				value = value.Replace("&gt;", ">");
				value = value.Replace("&amp;", "&");
			}
			return value;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00240808 File Offset: 0x0023FC08
		private string EncodeXmlValue(string value)
		{
			if (value != null && 0 < value.Length)
			{
				value = value.Replace('\0', ' ');
				value = value.Trim();
				value = value.Replace("&", "&amp;");
				value = value.Replace(">", "&gt;");
				value = value.Replace("<", "&lt;");
				value = value.Replace("'", "&apos;");
				value = value.Replace("\"", "&quot;");
			}
			return value;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00240898 File Offset: 0x0023FC98
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw ADP.ArgumentNull("securityElement");
			}
			string tag = securityElement.Tag;
			if (!tag.Equals("Permission") && !tag.Equals("IPermission"))
			{
				throw ADP.NotAPermissionElement();
			}
			string text = securityElement.Attribute("version");
			if (text != null && !text.Equals("1"))
			{
				throw ADP.InvalidXMLBadVersion();
			}
			string text2 = securityElement.Attribute("Unrestricted");
			this._isUnrestricted = (text2 != null && bool.Parse(text2));
			this.Clear();
			if (!this._isUnrestricted)
			{
				string text3 = securityElement.Attribute("AllowBlankPassword");
				this._allowBlankPassword = (text3 != null && bool.Parse(text3));
				ArrayList children = securityElement.Children;
				if (children == null)
				{
					return;
				}
				using (IEnumerator enumerator = children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement2 = (SecurityElement)obj;
						tag = securityElement2.Tag;
						if ("add" == tag || (tag != null && "add" == tag.ToLower(CultureInfo.InvariantCulture)))
						{
							string text4 = securityElement2.Attribute("ConnectionString");
							string text5 = securityElement2.Attribute("KeyRestrictions");
							string text6 = securityElement2.Attribute("KeyRestrictionBehavior");
							KeyRestrictionBehavior behavior = KeyRestrictionBehavior.AllowOnly;
							if (text6 != null)
							{
								behavior = (KeyRestrictionBehavior)Enum.Parse(typeof(KeyRestrictionBehavior), text6, true);
							}
							text4 = this.DecodeXmlValue(text4);
							text5 = this.DecodeXmlValue(text5);
							this.Add(text4, text5, behavior);
						}
					}
					return;
				}
			}
			this._allowBlankPassword = false;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00240A58 File Offset: 0x0023FE58
		public override SecurityElement ToXml()
		{
			Type type = base.GetType();
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.AssemblyQualifiedName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("AllowBlankPassword", this._allowBlankPassword.ToString(CultureInfo.InvariantCulture));
				if (this._keyvalues != null)
				{
					foreach (object obj in this._keyvalues)
					{
						DBConnectionString dbconnectionString = (DBConnectionString)obj;
						SecurityElement securityElement2 = new SecurityElement("add");
						string text = dbconnectionString.ConnectionString;
						text = this.EncodeXmlValue(text);
						if (!ADP.IsEmpty(text))
						{
							securityElement2.AddAttribute("ConnectionString", text);
						}
						text = dbconnectionString.Restrictions;
						text = this.EncodeXmlValue(text);
						if (text == null)
						{
							text = ADP.StrEmpty;
						}
						securityElement2.AddAttribute("KeyRestrictions", text);
						text = dbconnectionString.Behavior.ToString();
						securityElement2.AddAttribute("KeyRestrictionBehavior", text);
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x04000C4C RID: 3148
		private bool _isUnrestricted;

		// Token: 0x04000C4D RID: 3149
		private bool _allowBlankPassword;

		// Token: 0x04000C4E RID: 3150
		private NameValuePermission _keyvaluetree;

		// Token: 0x04000C4F RID: 3151
		private ArrayList _keyvalues;
	}
}
