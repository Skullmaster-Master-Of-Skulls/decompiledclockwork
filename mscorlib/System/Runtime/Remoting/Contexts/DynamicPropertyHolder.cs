using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020006D9 RID: 1753
	internal class DynamicPropertyHolder
	{
		// Token: 0x06003F16 RID: 16150 RVA: 0x000D8048 File Offset: 0x000D7048
		internal virtual bool AddDynamicProperty(IDynamicProperty prop)
		{
			bool result;
			lock (this)
			{
				DynamicPropertyHolder.CheckPropertyNameClash(prop.Name, this._props, this._numProps);
				bool flag = false;
				if (this._props == null || this._numProps == this._props.Length)
				{
					this._props = DynamicPropertyHolder.GrowPropertiesArray(this._props);
					flag = true;
				}
				this._props[this._numProps++] = prop;
				if (flag)
				{
					this._sinks = DynamicPropertyHolder.GrowDynamicSinksArray(this._sinks);
				}
				if (this._sinks == null)
				{
					this._sinks = new IDynamicMessageSink[this._props.Length];
					for (int i = 0; i < this._numProps; i++)
					{
						this._sinks[i] = ((IContributeDynamicSink)this._props[i]).GetDynamicSink();
					}
				}
				else
				{
					this._sinks[this._numProps - 1] = ((IContributeDynamicSink)prop).GetDynamicSink();
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000D8150 File Offset: 0x000D7150
		internal virtual bool RemoveDynamicProperty(string name)
		{
			lock (this)
			{
				for (int i = 0; i < this._numProps; i++)
				{
					if (this._props[i].Name.Equals(name))
					{
						this._props[i] = this._props[this._numProps - 1];
						this._numProps--;
						this._sinks = null;
						return true;
					}
				}
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), new object[]
				{
					name
				}));
			}
			bool result;
			return result;
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000D81FC File Offset: 0x000D71FC
		internal virtual IDynamicProperty[] DynamicProperties
		{
			get
			{
				if (this._props == null)
				{
					return null;
				}
				IDynamicProperty[] result;
				lock (this)
				{
					IDynamicProperty[] array = new IDynamicProperty[this._numProps];
					Array.Copy(this._props, array, this._numProps);
					result = array;
				}
				return result;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x000D8258 File Offset: 0x000D7258
		internal virtual ArrayWithSize DynamicSinks
		{
			get
			{
				if (this._numProps == 0)
				{
					return null;
				}
				lock (this)
				{
					if (this._sinks == null)
					{
						this._sinks = new IDynamicMessageSink[this._numProps + 8];
						for (int i = 0; i < this._numProps; i++)
						{
							this._sinks[i] = ((IContributeDynamicSink)this._props[i]).GetDynamicSink();
						}
					}
				}
				return new ArrayWithSize(this._sinks, this._numProps);
			}
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000D82E8 File Offset: 0x000D72E8
		private static IDynamicMessageSink[] GrowDynamicSinksArray(IDynamicMessageSink[] sinks)
		{
			int num = ((sinks != null) ? sinks.Length : 0) + 8;
			IDynamicMessageSink[] array = new IDynamicMessageSink[num];
			if (sinks != null)
			{
				Array.Copy(sinks, array, sinks.Length);
			}
			return array;
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000D8318 File Offset: 0x000D7318
		internal static void NotifyDynamicSinks(IMessage msg, ArrayWithSize dynSinks, bool bCliSide, bool bStart, bool bAsync)
		{
			for (int i = 0; i < dynSinks.Count; i++)
			{
				if (bStart)
				{
					dynSinks.Sinks[i].ProcessMessageStart(msg, bCliSide, bAsync);
				}
				else
				{
					dynSinks.Sinks[i].ProcessMessageFinish(msg, bCliSide, bAsync);
				}
			}
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x000D8360 File Offset: 0x000D7360
		internal static void CheckPropertyNameClash(string name, IDynamicProperty[] props, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (props[i].Name.Equals(name))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
				}
			}
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x000D839C File Offset: 0x000D739C
		internal static IDynamicProperty[] GrowPropertiesArray(IDynamicProperty[] props)
		{
			int num = ((props != null) ? props.Length : 0) + 8;
			IDynamicProperty[] array = new IDynamicProperty[num];
			if (props != null)
			{
				Array.Copy(props, array, props.Length);
			}
			return array;
		}

		// Token: 0x04002005 RID: 8197
		private const int GROW_BY = 8;

		// Token: 0x04002006 RID: 8198
		private IDynamicProperty[] _props;

		// Token: 0x04002007 RID: 8199
		private int _numProps;

		// Token: 0x04002008 RID: 8200
		private IDynamicMessageSink[] _sinks;
	}
}
