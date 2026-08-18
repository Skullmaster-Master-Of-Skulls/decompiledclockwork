using System;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007CB RID: 1995
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cascadable")]
	public abstract class CascadableNavigationPropertyConfiguration
	{
		// Token: 0x06005A93 RID: 23187 RVA: 0x001866C8 File Offset: 0x001848C8
		internal CascadableNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x001866D7 File Offset: 0x001848D7
		public void WillCascadeOnDelete()
		{
			this.WillCascadeOnDelete(true);
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x001866E0 File Offset: 0x001848E0
		public void WillCascadeOnDelete(bool value)
		{
			this._navigationPropertyConfiguration.DeleteAction = new OperationAction?(value ? OperationAction.Cascade : OperationAction.None);
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005A96 RID: 23190 RVA: 0x001866F9 File Offset: 0x001848F9
		internal NavigationPropertyConfiguration NavigationPropertyConfiguration
		{
			get
			{
				return this._navigationPropertyConfiguration;
			}
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x00186701 File Offset: 0x00184901
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x00186709 File Offset: 0x00184909
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x00186712 File Offset: 0x00184912
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x0018671A File Offset: 0x0018491A
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400241F RID: 9247
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}
