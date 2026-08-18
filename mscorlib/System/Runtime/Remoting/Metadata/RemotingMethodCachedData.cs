using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x02000743 RID: 1859
	internal class RemotingMethodCachedData : RemotingCachedData
	{
		// Token: 0x0600427C RID: 17020 RVA: 0x000E248C File Offset: 0x000E148C
		internal RemotingMethodCachedData(object ri) : base(ri)
		{
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x000E2495 File Offset: 0x000E1495
		internal string TypeAndAssemblyName
		{
			get
			{
				if (this._typeAndAssemblyName == null)
				{
					this.UpdateNames();
				}
				return this._typeAndAssemblyName;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x000E24AB File Offset: 0x000E14AB
		internal string MethodName
		{
			get
			{
				if (this._methodName == null)
				{
					this.UpdateNames();
				}
				return this._methodName;
			}
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000E24C4 File Offset: 0x000E14C4
		private void UpdateNames()
		{
			MethodBase methodBase = (MethodBase)this.RI;
			this._methodName = methodBase.Name;
			if (methodBase.DeclaringType != null)
			{
				this._typeAndAssemblyName = RemotingServices.GetDefaultQualifiedTypeName(methodBase.DeclaringType);
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000E2502 File Offset: 0x000E1502
		internal ParameterInfo[] Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = ((MethodBase)this.RI).GetParameters();
				}
				return this._parameters;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x000E2528 File Offset: 0x000E1528
		internal int[] OutRefArgMap
		{
			get
			{
				if (this._outRefArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outRefArgMap;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x000E253E File Offset: 0x000E153E
		internal int[] OutOnlyArgMap
		{
			get
			{
				if (this._outOnlyArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outOnlyArgMap;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x000E2554 File Offset: 0x000E1554
		internal int[] NonRefOutArgMap
		{
			get
			{
				if (this._nonRefOutArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._nonRefOutArgMap;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x000E256A File Offset: 0x000E156A
		internal int[] MarshalRequestArgMap
		{
			get
			{
				if (this._marshalRequestMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalRequestMap;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x000E2580 File Offset: 0x000E1580
		internal int[] MarshalResponseArgMap
		{
			get
			{
				if (this._marshalResponseMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalResponseMap;
			}
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000E2598 File Offset: 0x000E1598
		private void GetArgMaps()
		{
			lock (this)
			{
				if (this._inRefArgMap == null)
				{
					int[] inRefArgMap = null;
					int[] outRefArgMap = null;
					int[] outOnlyArgMap = null;
					int[] nonRefOutArgMap = null;
					int[] marshalRequestMap = null;
					int[] marshalResponseMap = null;
					ArgMapper.GetParameterMaps(this.Parameters, out inRefArgMap, out outRefArgMap, out outOnlyArgMap, out nonRefOutArgMap, out marshalRequestMap, out marshalResponseMap);
					this._inRefArgMap = inRefArgMap;
					this._outRefArgMap = outRefArgMap;
					this._outOnlyArgMap = outOnlyArgMap;
					this._nonRefOutArgMap = nonRefOutArgMap;
					this._marshalRequestMap = marshalRequestMap;
					this._marshalResponseMap = marshalResponseMap;
				}
			}
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000E2624 File Offset: 0x000E1624
		internal bool IsOneWayMethod()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay;
				object[] customAttributes = ((ICustomAttributeProvider)this.RI).GetCustomAttributes(typeof(OneWayAttribute), true);
				if (customAttributes != null && customAttributes.Length > 0)
				{
					methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOneWay;
				}
				this.flags |= methodCacheFlags;
				return (methodCacheFlags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) != RemotingMethodCachedData.MethodCacheFlags.None;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) != RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x000E268C File Offset: 0x000E168C
		internal bool IsOverloaded()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded;
				MethodBase methodBase = (MethodBase)this.RI;
				if (methodBase.IsOverloaded)
				{
					methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
				}
				this.flags |= methodCacheFlags;
				return (methodCacheFlags & RemotingMethodCachedData.MethodCacheFlags.IsOverloaded) != RemotingMethodCachedData.MethodCacheFlags.None;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOverloaded) != RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x000E26E4 File Offset: 0x000E16E4
		internal Type ReturnType
		{
			get
			{
				if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType) == RemotingMethodCachedData.MethodCacheFlags.None)
				{
					MethodInfo methodInfo = this.RI as MethodInfo;
					if (methodInfo != null)
					{
						Type returnType = methodInfo.ReturnType;
						if (returnType != typeof(void))
						{
							this._returnType = returnType;
						}
					}
					this.flags |= RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType;
				}
				return this._returnType;
			}
		}

		// Token: 0x0400214F RID: 8527
		private ParameterInfo[] _parameters;

		// Token: 0x04002150 RID: 8528
		private RemotingMethodCachedData.MethodCacheFlags flags;

		// Token: 0x04002151 RID: 8529
		private string _typeAndAssemblyName;

		// Token: 0x04002152 RID: 8530
		private string _methodName;

		// Token: 0x04002153 RID: 8531
		private Type _returnType;

		// Token: 0x04002154 RID: 8532
		private int[] _inRefArgMap;

		// Token: 0x04002155 RID: 8533
		private int[] _outRefArgMap;

		// Token: 0x04002156 RID: 8534
		private int[] _outOnlyArgMap;

		// Token: 0x04002157 RID: 8535
		private int[] _nonRefOutArgMap;

		// Token: 0x04002158 RID: 8536
		private int[] _marshalRequestMap;

		// Token: 0x04002159 RID: 8537
		private int[] _marshalResponseMap;

		// Token: 0x02000744 RID: 1860
		[Flags]
		[Serializable]
		private enum MethodCacheFlags
		{
			// Token: 0x0400215B RID: 8539
			None = 0,
			// Token: 0x0400215C RID: 8540
			CheckedOneWay = 1,
			// Token: 0x0400215D RID: 8541
			IsOneWay = 2,
			// Token: 0x0400215E RID: 8542
			CheckedOverloaded = 4,
			// Token: 0x0400215F RID: 8543
			IsOverloaded = 8,
			// Token: 0x04002160 RID: 8544
			CheckedForAsync = 16,
			// Token: 0x04002161 RID: 8545
			CheckedForReturnType = 32
		}
	}
}
