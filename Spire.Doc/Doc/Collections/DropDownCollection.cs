using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x0200052A RID: 1322
	public class DropDownCollection : DocumentSerializableCollection
	{
		// Token: 0x17000531 RID: 1329
		public DropDownItem this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (DropDownItem)base.InnerList[index];
			}
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x004072D0 File Offset: 0x004062D0
		public DropDownCollection(Document doc) : base(doc, null)
		{
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x004072E8 File Offset: 0x004062E8
		public DropDownItem Add(string text)
		{
			int a_ = 19;
			while (base.InnerList.Count > 24)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				throw new ArgumentException(ClipboardData.b("⵸፺᡼彾ꒈﾐ뎒璉ﺖ붜咽캠톢좤螦쾨슪좬쎮햰鎲풴\udbb6햸풺쪼첾뫂꫄닆뿊ꋌ뻐뷒맔껖律뫚맜믞쇠苢엤諦裨鏪蓬苮蓰黲헴飶鿸\udbfa쿼쫾℀樂焄戆搈砊ⴌ笎縐㌒愔编簘㬚礜洞丠匢ࠤ䌦䘨尪䌬༮崰娲䘴䌶᜸", a_));
			}
			DropDownItem dropDownItem = new DropDownItem(base.Document);
			dropDownItem.Text = text;
			base.InnerList.Add(dropDownItem);
			return dropDownItem;
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x00407374 File Offset: 0x00406374
		public void Remove(int index)
		{
			int a_ = 2;
			while (index >= base.InnerList.Count)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					throw new ArgumentException(ClipboardData.b("Ⱨᡩͫṭ㑯ᵱͳᡵㅷ๹᥻፽ꁿꪉﾋﮍ晴뒓ﾕﺙ鍊肟욡쮣쎥\udba7쒩讫\udaad邯ힱ첳\udfb5쮷캹銻", a_));
				}
			}
			DropDownItem value = (DropDownItem)base.InnerList[index];
			base.InnerList.Remove(value);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x004073FC File Offset: 0x004063FC
		public void Clear()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.InnerList.Clear();
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x00407444 File Offset: 0x00406444
		internal int ᜀ(DropDownItem A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return base.InnerList.Add(A_0);
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x0040748C File Offset: 0x0040648C
		internal void ᜀ(DropDownCollection A_0)
		{
			for (;;)
			{
				IL_18:
				int num = 0;
				int count = base.Count;
				for (;;)
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_51;
						case 1:
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							A_0.ᜀ(this[num].ᜀ());
							num++;
							num2 = 3;
							continue;
						case 2:
							if (true)
							{
							}
							goto IL_3D;
						case 3:
							goto IL_3D;
						}
						goto IL_18;
						IL_3D:
						num2 = 1;
					}
					IL_51:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_8A;
					}
				}
			}
			IL_8A:
			if (false)
			{
			}
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x0040752C File Offset: 0x0040652C
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new DropDownItem(base.Document);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x00407574 File Offset: 0x00406574
		protected override string GetTagItemName()
		{
			int a_ = 4;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("๩ṫŭoᙱ᭳ŵᙷ坹ᕻ੽", a_);
		}
	}
}
