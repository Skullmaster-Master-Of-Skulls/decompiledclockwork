using System;
using System.Collections;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020012BA RID: 4794
	public class MaskPartCollection : CollectionBase, IStateManager
	{
		// Token: 0x170040D5 RID: 16597
		// (get) Token: 0x0600C8A3 RID: 51363 RVA: 0x002CC001 File Offset: 0x002CA201
		// (set) Token: 0x0600C8A4 RID: 51364 RVA: 0x002CC009 File Offset: 0x002CA209
		public RadMaskedTextBox Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x170040D6 RID: 16598
		// (get) Token: 0x0600C8A5 RID: 51365 RVA: 0x002CC012 File Offset: 0x002CA212
		// (set) Token: 0x0600C8A6 RID: 51366 RVA: 0x002CC01A File Offset: 0x002CA21A
		public MaskedTextBoxSetting OwnerMaskedTextBoxSetting
		{
			get
			{
				return this._ownerMaskedTextBoxSetting;
			}
			set
			{
				this._ownerMaskedTextBoxSetting = value;
			}
		}

		// Token: 0x0600C8A7 RID: 51367 RVA: 0x002CC023 File Offset: 0x002CA223
		public void Add(MaskPart part)
		{
			part.Input = this.Owner;
			part.MaskedTextBoxSetting = this.OwnerMaskedTextBoxSetting;
			base.List.Add(part);
		}

		// Token: 0x0600C8A8 RID: 51368 RVA: 0x002CC04A File Offset: 0x002CA24A
		public void Insert(int index, MaskPart part)
		{
			part.Input = this.Owner;
			part.MaskedTextBoxSetting = this.OwnerMaskedTextBoxSetting;
			base.List.Insert(index, part);
		}

		// Token: 0x0600C8A9 RID: 51369 RVA: 0x002CC071 File Offset: 0x002CA271
		public bool Contains(MaskPart part)
		{
			return base.List.Contains(part);
		}

		// Token: 0x0600C8AA RID: 51370 RVA: 0x002CC07F File Offset: 0x002CA27F
		public void Remove(MaskPart part)
		{
			base.List.Remove(part);
		}

		// Token: 0x0600C8AB RID: 51371 RVA: 0x002CC08D File Offset: 0x002CA28D
		public int IndexOf(MaskPart part)
		{
			return base.List.IndexOf(part);
		}

		// Token: 0x170040D7 RID: 16599
		public MaskPart this[int index]
		{
			get
			{
				return (MaskPart)base.List[index];
			}
		}

		// Token: 0x170040D8 RID: 16600
		// (get) Token: 0x0600C8AD RID: 51373 RVA: 0x002CC0B0 File Offset: 0x002CA2B0
		// (set) Token: 0x0600C8AE RID: 51374 RVA: 0x002CC118 File Offset: 0x002CA318
		internal string Mask
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this)
				{
					MaskPart maskPart = (MaskPart)obj;
					stringBuilder.Append(maskPart.Part);
				}
				return stringBuilder.ToString();
			}
			set
			{
				base.List.Clear();
				MaskPartCollection maskPartCollection = new MaskParser().Parse(value);
				foreach (object obj in maskPartCollection)
				{
					MaskPart part = (MaskPart)obj;
					this.Add(part);
				}
			}
		}

		// Token: 0x170040D9 RID: 16601
		// (get) Token: 0x0600C8AF RID: 51375 RVA: 0x002CC184 File Offset: 0x002CA384
		internal string Prompt
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in base.List)
				{
					MaskPart maskPart = (MaskPart)obj;
					stringBuilder.Append(maskPart.Prompt);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600C8B0 RID: 51376 RVA: 0x002CC1F0 File Offset: 0x002CA3F0
		void IStateManager.LoadViewState(object state)
		{
			this.Mask = (string)state;
		}

		// Token: 0x0600C8B1 RID: 51377 RVA: 0x002CC1FE File Offset: 0x002CA3FE
		object IStateManager.SaveViewState()
		{
			return this.Mask;
		}

		// Token: 0x0600C8B2 RID: 51378 RVA: 0x002CC206 File Offset: 0x002CA406
		void IStateManager.TrackViewState()
		{
			this._trackingState = true;
		}

		// Token: 0x170040DA RID: 16602
		// (get) Token: 0x0600C8B3 RID: 51379 RVA: 0x002CC20F File Offset: 0x002CA40F
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._trackingState;
			}
		}

		// Token: 0x040034CE RID: 13518
		private bool _trackingState;

		// Token: 0x040034CF RID: 13519
		private RadMaskedTextBox _owner;

		// Token: 0x040034D0 RID: 13520
		private MaskedTextBoxSetting _ownerMaskedTextBoxSetting;
	}
}
