using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002BE RID: 702
	public class MailMergeCode : ICloneable<MailMergeCode>, ICloneable
	{
		// Token: 0x0600150E RID: 5390 RVA: 0x0001A4E7 File Offset: 0x000186E7
		public MailMergeCode()
		{
			this.ValueFormat = new MailMergeValueFormat();
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0001A4FD File Offset: 0x000186FD
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x0001A505 File Offset: 0x00018705
		public string Name { get; set; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x0001A50E File Offset: 0x0001870E
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x0001A516 File Offset: 0x00018716
		public string OriginalCode { get; set; }

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0001A51F File Offset: 0x0001871F
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x0001A527 File Offset: 0x00018727
		public Dictionary<string, string> Args { get; set; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0001A530 File Offset: 0x00018730
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x0001A538 File Offset: 0x00018738
		public MailMergeValueFormat ValueFormat { get; set; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0001A541 File Offset: 0x00018741
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x0001A549 File Offset: 0x00018749
		public string DefaultValue { get; set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0001A552 File Offset: 0x00018752
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x0001A55A File Offset: 0x0001875A
		public int AltPersonIdIndex { get; set; }

		// Token: 0x0600151B RID: 5403 RVA: 0x0001A564 File Offset: 0x00018764
		public void SetValueFormatIfNotOverridenByUser(MailMergeValueFormat valFormat)
		{
			bool flag = this.ValueFormat == null || (this.ValueFormat.ValueFormatType == eValueFormatType.DefaultToStringFormat && string.IsNullOrEmpty(this.ValueFormat.CustomFormat));
			if (flag)
			{
				this.ValueFormat = valFormat;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0001A5A9 File Offset: 0x000187A9
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x0001A5B1 File Offset: 0x000187B1
		private IList<MailMergeValueBase> MailMergeValues { get; set; }

		// Token: 0x0600151E RID: 5406 RVA: 0x0001A5BC File Offset: 0x000187BC
		public bool IsOfType<T>() where T : MailMergeValueBase
		{
			bool flag = this.MailMergeValues == null || this.MailMergeValues.Count < 1;
			return !flag && this.MailMergeValues[0] is T;
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0001A604 File Offset: 0x00018804
		public bool MailMergeValueIsNull
		{
			get
			{
				return this.MailMergeValues == null || this.MailMergeValues.Count < 1;
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0001A630 File Offset: 0x00018830
		private void SetMailMergeValue<T>(object obj) where T : MailMergeValueBase
		{
			bool flag = obj != null && obj is IList && !(obj is byte[]);
			if (flag)
			{
				this.SetMailMergeValues<T>(((IList)obj).Cast<object>().ToList<object>());
			}
			else
			{
				T t = Activator.CreateInstance<T>();
				t.SetValue(obj);
				this.MailMergeValues = new List<T>
				{
					t
				}.Cast<MailMergeValueBase>().ToList<MailMergeValueBase>();
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0001A6A8 File Offset: 0x000188A8
		private void SetMailMergeValues<T>(IList<object> vals) where T : MailMergeValueBase
		{
			List<T> list = new List<T>();
			foreach (object value in vals)
			{
				T t = Activator.CreateInstance<T>();
				t.SetValue(value);
				list.Add(t);
			}
			this.MailMergeValues = list.Cast<MailMergeValueBase>().ToList<MailMergeValueBase>();
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0001A724 File Offset: 0x00018924
		public void SetMailMergeValue(int num)
		{
			this.SetMailMergeValue<MailMergeValueInt>(num);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0001A734 File Offset: 0x00018934
		public void SetMailMergeValue(MailMergeCheckedItem checkedItem)
		{
			this.SetMailMergeValue<MailMergeValueCheckedItem>(checkedItem);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0001A73F File Offset: 0x0001893F
		public void SetMailMergeValue(IList<int> nums)
		{
			this.SetMailMergeValue<MailMergeValueInt>(nums);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0001A74A File Offset: 0x0001894A
		public void SetMailMergeValue(DateTime dateTime)
		{
			this.SetMailMergeValue<MailMergeValueDateTime>(dateTime);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0001A75A File Offset: 0x0001895A
		public void SetMailMergeValue(string str)
		{
			this.SetMailMergeValue<MailMergeValueString>(str);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0001A75A File Offset: 0x0001895A
		public void SetMailMergeValue(IList<string> strs)
		{
			this.SetMailMergeValue<MailMergeValueString>(strs);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0001A765 File Offset: 0x00018965
		public void SetMailMergeValue(bool b)
		{
			this.SetMailMergeValue<MailMergeValueBool>(b);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0001A775 File Offset: 0x00018975
		public void SetMailMergeValue(AccommodationData accommodationData)
		{
			this.SetMailMergeValue<MailMergeValueAccommodationData>(accommodationData);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0001A775 File Offset: 0x00018975
		public void SetMailMergeValue(IList<AccommodationData> accommodationDatas)
		{
			this.SetMailMergeValue<MailMergeValueAccommodationData>(accommodationDatas);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0001A780 File Offset: 0x00018980
		public void SetMailMergeValue(DynamicData dynamicData)
		{
			this.SetMailMergeValue<MailMergeValueDynamicData>(dynamicData);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0001A780 File Offset: 0x00018980
		public void SetMailMergeValue(IList<DynamicData> dynamicData)
		{
			this.SetMailMergeValue<MailMergeValueDynamicData>(dynamicData);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0001A78B File Offset: 0x0001898B
		public void SetMailMergeValue(byte[] byteArray)
		{
			this.SetMailMergeValue<MailMergeValueByteArray>(byteArray);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0001A796 File Offset: 0x00018996
		public void SetMailMergeValue(double num)
		{
			this.SetMailMergeValue<MailMergeValueDouble>(num);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0001A7A6 File Offset: 0x000189A6
		public void SetMailMergeValue(DateTime? dateTimeNullable)
		{
			this.SetMailMergeValue<MailMergeValueDateTimeNullable>(dateTimeNullable);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0001A7B8 File Offset: 0x000189B8
		public IList<MailMergeValueBase> GetMailMergeValuesDirectly()
		{
			return this.MailMergeValues;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0001A7D0 File Offset: 0x000189D0
		public string GetFirstMailMergeValueAsString()
		{
			MailMergeValueBase mailMergeValueBase = this.GetMailMergeValuesDirectly().FirstOrDefault<MailMergeValueBase>();
			object obj = (mailMergeValueBase != null) ? mailMergeValueBase.GetValue() : null;
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = obj as string;
				bool flag2 = text != null;
				if (flag2)
				{
					result = text;
				}
				else
				{
					result = obj.ToString();
				}
			}
			return result;
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0001A82C File Offset: 0x00018A2C
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x0001A844 File Offset: 0x00018A44
		public IList<MailMergeValueBase> MailMergeValueSetterGetter
		{
			get
			{
				return this.MailMergeValues;
			}
			set
			{
				this.MailMergeValues = value;
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0001A84F File Offset: 0x00018A4F
		public void SetMailMergeValueDirectly(IList<MailMergeValueBase> newValue)
		{
			this.MailMergeValueSetterGetter = newValue;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0001A85C File Offset: 0x00018A5C
		public bool IsValueAList()
		{
			return this.MailMergeValues != null && this.MailMergeValues.Count > 1;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0001A888 File Offset: 0x00018A88
		public R GetFirstMailMergeValue<T, R>(R defaultValue) where T : MailMergeValueBase
		{
			IList<MailMergeValueBase> mailMergeValues = this.MailMergeValues;
			List<MailMergeValueBase> list;
			if (mailMergeValues == null)
			{
				list = null;
			}
			else
			{
				list = (from g in mailMergeValues
				where g is T
				select g).ToList<MailMergeValueBase>();
			}
			List<MailMergeValueBase> list2 = list ?? new List<MailMergeValueBase>();
			bool flag = list2.Count < 1;
			R result;
			if (flag)
			{
				bool flag2 = typeof(T) == typeof(MailMergeValueString);
				if (flag2)
				{
					IList<MailMergeValueBase> mailMergeValues2 = this.MailMergeValues;
					list2 = (((mailMergeValues2 != null) ? mailMergeValues2.ToList<MailMergeValueBase>() : null) ?? new List<MailMergeValueBase>());
					bool flag3 = list2.Count > 0;
					if (flag3)
					{
						MailMergeValueBase mailMergeValueBase = list2[0];
						object value = mailMergeValueBase.GetValue();
						return (value == null) ? defaultValue : ((R)((object)value.ToString()));
					}
				}
				result = defaultValue;
			}
			else
			{
				MailMergeValueBase mailMergeValueBase2 = list2[0];
				result = mailMergeValueBase2.GetValue<R>(mailMergeValueBase2.GetValue(), defaultValue);
			}
			return result;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0001A978 File Offset: 0x00018B78
		public IList<R> GetMailMergeValues<T, R>(R defaultValue) where T : MailMergeValueBase where R : class
		{
			List<MailMergeValueBase> list;
			if (this.MailMergeValues != null)
			{
				list = (from g in this.MailMergeValues
				where g is T
				select g).ToList<MailMergeValueBase>();
			}
			else
			{
				list = new List<MailMergeValueBase>();
			}
			List<MailMergeValueBase> list2 = list;
			bool flag = list2.Count < 1;
			IList<R> result;
			if (flag)
			{
				result = new List<R>();
			}
			else
			{
				List<R> list3 = new List<R>();
				foreach (MailMergeValueBase mailMergeValueBase in list2)
				{
					R value = mailMergeValueBase.GetValue<R>(mailMergeValueBase.GetValue(), defaultValue);
					bool flag2 = value != null;
					if (flag2)
					{
						list3.Add(value);
					}
				}
				result = list3;
			}
			return result;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0001AA50 File Offset: 0x00018C50
		public MailMergeCode Clone()
		{
			return new MailMergeCode(this);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0001AA68 File Offset: 0x00018C68
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0001AA80 File Offset: 0x00018C80
		public MailMergeCode(MailMergeCode item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Name = item.Name;
				this.OriginalCode = item.OriginalCode;
				bool flag2 = item.Args != null;
				if (flag2)
				{
					this.Args = new Dictionary<string, string>();
					foreach (KeyValuePair<string, string> keyValuePair in item.Args)
					{
						this.Args.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				MailMergeValueFormat valueFormat = item.ValueFormat;
				this.ValueFormat = ((valueFormat != null) ? valueFormat.Clone() : null);
				this.DefaultValue = item.DefaultValue;
				IList<MailMergeValueBase> mailMergeValues = item.MailMergeValues;
				IList<MailMergeValueBase> mailMergeValues2;
				if (mailMergeValues == null)
				{
					mailMergeValues2 = null;
				}
				else
				{
					mailMergeValues2 = (from g in mailMergeValues
					select g.Clone()).ToList<MailMergeValueBase>();
				}
				this.MailMergeValues = mailMergeValues2;
			}
		}
	}
}
