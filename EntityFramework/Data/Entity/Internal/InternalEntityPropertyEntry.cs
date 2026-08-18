using System;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200077A RID: 1914
	internal class InternalEntityPropertyEntry : InternalPropertyEntry
	{
		// Token: 0x060056D2 RID: 22226 RVA: 0x00177A90 File Offset: 0x00175C90
		public InternalEntityPropertyEntry(InternalEntityEntry internalEntityEntry, PropertyEntryMetadata propertyMetadata) : base(internalEntityEntry, propertyMetadata)
		{
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060056D3 RID: 22227 RVA: 0x00177A9A File Offset: 0x00175C9A
		public override InternalPropertyEntry ParentPropertyEntry
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060056D4 RID: 22228 RVA: 0x00177A9D File Offset: 0x00175C9D
		public override InternalPropertyValues ParentCurrentValues
		{
			get
			{
				return this.InternalEntityEntry.CurrentValues;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060056D5 RID: 22229 RVA: 0x00177AAA File Offset: 0x00175CAA
		public override InternalPropertyValues ParentOriginalValues
		{
			get
			{
				return this.InternalEntityEntry.OriginalValues;
			}
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x00177AB8 File Offset: 0x00175CB8
		protected override Func<object, object> CreateGetter()
		{
			Func<object, object> result;
			DbHelpers.GetPropertyGetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out result);
			return result;
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x00177AE4 File Offset: 0x00175CE4
		protected override Action<object, object> CreateSetter()
		{
			Action<object, object> result;
			DbHelpers.GetPropertySetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out result);
			return result;
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x00177B10 File Offset: 0x00175D10
		public override bool EntityPropertyIsModified()
		{
			return this.InternalEntityEntry.ObjectStateEntry.GetModifiedProperties().Contains(this.Name);
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x00177B2D File Offset: 0x00175D2D
		public override void SetEntityPropertyModified()
		{
			this.InternalEntityEntry.ObjectStateEntry.SetModifiedProperty(this.Name);
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x00177B45 File Offset: 0x00175D45
		public override void RejectEntityPropertyChanges()
		{
			this.InternalEntityEntry.ObjectStateEntry.RejectPropertyChanges(this.Name);
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x00177B5D File Offset: 0x00175D5D
		public override void UpdateComplexPropertyState()
		{
			if (!this.InternalEntityEntry.ObjectStateEntry.IsPropertyChanged(this.Name))
			{
				this.RejectEntityPropertyChanges();
			}
		}
	}
}
