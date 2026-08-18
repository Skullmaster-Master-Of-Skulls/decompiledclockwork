using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace System
{
	// Token: 0x02000019 RID: 25
	internal class UriTemplateTrieNode
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000058C8 File Offset: 0x00003AC8
		private UriTemplateTrieNode(int depth)
		{
			this.depth = depth;
			this.nextLiteralSegment = null;
			this.nextCompoundSegment = null;
			this.finalLiteralSegment = null;
			this.finalCompoundSegment = null;
			this.finalVariableSegment = new UriTemplatePathPartiallyEquivalentSet(depth + 1);
			this.star = new UriTemplatePathPartiallyEquivalentSet(depth);
			this.endOfPath = new UriTemplatePathPartiallyEquivalentSet(depth);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005924 File Offset: 0x00003B24
		public static UriTemplateTrieNode Make(IEnumerable<KeyValuePair<UriTemplate, object>> keyValuePairs, bool allowDuplicateEquivalentUriTemplates)
		{
			UriTemplateTrieNode uriTemplateTrieNode = new UriTemplateTrieNode(0);
			foreach (KeyValuePair<UriTemplate, object> kvp in keyValuePairs)
			{
				UriTemplateTrieNode.Add(uriTemplateTrieNode, kvp);
			}
			UriTemplateTrieNode.Validate(uriTemplateTrieNode, allowDuplicateEquivalentUriTemplates);
			return uriTemplateTrieNode;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000597C File Offset: 0x00003B7C
		public bool Match(UriTemplateLiteralPathSegment[] wireData, ICollection<UriTemplateTableMatchCandidate> candidates)
		{
			UriTemplateTrieLocation location = new UriTemplateTrieLocation(this, UriTemplateTrieIntraNodeLocation.BeforeLiteral);
			return UriTemplateTrieNode.GetMatch(location, wireData, candidates);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000599C File Offset: 0x00003B9C
		private static void Add(UriTemplateTrieNode root, KeyValuePair<UriTemplate, object> kvp)
		{
			UriTemplateTrieNode uriTemplateTrieNode = root;
			UriTemplate key = kvp.Key;
			bool flag = key.segments.Count == 0 || key.HasWildcard || key.segments[key.segments.Count - 1].EndsWithSlash;
			for (int i = 0; i < key.segments.Count; i++)
			{
				if (i >= key.firstOptionalSegment)
				{
					uriTemplateTrieNode.endOfPath.Items.Add(kvp);
				}
				UriTemplatePathSegment uriTemplatePathSegment = key.segments[i];
				if (!uriTemplatePathSegment.EndsWithSlash)
				{
					switch (uriTemplatePathSegment.Nature)
					{
					case UriTemplatePartType.Literal:
						uriTemplateTrieNode.AddFinalLiteralSegment(uriTemplatePathSegment as UriTemplateLiteralPathSegment, kvp);
						break;
					case UriTemplatePartType.Compound:
						uriTemplateTrieNode.AddFinalCompoundSegment(uriTemplatePathSegment as UriTemplateCompoundPathSegment, kvp);
						break;
					case UriTemplatePartType.Variable:
						uriTemplateTrieNode.finalVariableSegment.Items.Add(kvp);
						break;
					}
				}
				else
				{
					switch (uriTemplatePathSegment.Nature)
					{
					case UriTemplatePartType.Literal:
						uriTemplateTrieNode = uriTemplateTrieNode.AddNextLiteralSegment(uriTemplatePathSegment as UriTemplateLiteralPathSegment);
						break;
					case UriTemplatePartType.Compound:
						uriTemplateTrieNode = uriTemplateTrieNode.AddNextCompoundSegment(uriTemplatePathSegment as UriTemplateCompoundPathSegment);
						break;
					case UriTemplatePartType.Variable:
						uriTemplateTrieNode = uriTemplateTrieNode.AddNextVariableSegment();
						break;
					}
				}
			}
			if (flag)
			{
				if (key.HasWildcard)
				{
					uriTemplateTrieNode.star.Items.Add(kvp);
					return;
				}
				uriTemplateTrieNode.endOfPath.Items.Add(kvp);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005AFC File Offset: 0x00003CFC
		private static bool CheckMultipleMatches(IList<IList<UriTemplateTrieLocation>> locationsSet, UriTemplateLiteralPathSegment[] wireData, ICollection<UriTemplateTableMatchCandidate> candidates)
		{
			bool flag = false;
			int num = 0;
			while (num < locationsSet.Count && !flag)
			{
				for (int i = 0; i < locationsSet[num].Count; i++)
				{
					if (UriTemplateTrieNode.GetMatch(locationsSet[num][i], wireData, candidates))
					{
						flag = true;
					}
				}
				num++;
			}
			return flag;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005B50 File Offset: 0x00003D50
		private static bool GetMatch(UriTemplateTrieLocation location, UriTemplateLiteralPathSegment[] wireData, ICollection<UriTemplateTableMatchCandidate> candidates)
		{
			int num = location.node.depth;
			UriTemplatePathPartiallyEquivalentSet uriTemplatePathPartiallyEquivalentSet;
			UriTemplateTrieNode.SingleLocationOrLocationsSet singleLocationOrLocationsSet;
			while (!UriTemplateTrieNode.TryMatch(wireData, location, out uriTemplatePathPartiallyEquivalentSet, out singleLocationOrLocationsSet))
			{
				if (singleLocationOrLocationsSet.IsSingle)
				{
					location = singleLocationOrLocationsSet.SingleLocation;
				}
				else
				{
					if (UriTemplateTrieNode.CheckMultipleMatches(singleLocationOrLocationsSet.LocationsSet, wireData, candidates))
					{
						return true;
					}
					location = UriTemplateTrieNode.GetFailureLocationFromLocationsSet(singleLocationOrLocationsSet.LocationsSet);
				}
				if (location == null || location.node.depth < num)
				{
					return false;
				}
			}
			if (uriTemplatePathPartiallyEquivalentSet != null)
			{
				for (int i = 0; i < uriTemplatePathPartiallyEquivalentSet.Items.Count; i++)
				{
					candidates.Add(new UriTemplateTableMatchCandidate(uriTemplatePathPartiallyEquivalentSet.Items[i].Key, uriTemplatePathPartiallyEquivalentSet.SegmentsCount, uriTemplatePathPartiallyEquivalentSet.Items[i].Value));
				}
			}
			return true;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005C18 File Offset: 0x00003E18
		private static bool TryMatch(UriTemplateLiteralPathSegment[] wireUriSegments, UriTemplateTrieLocation currentLocation, out UriTemplatePathPartiallyEquivalentSet success, out UriTemplateTrieNode.SingleLocationOrLocationsSet nextStep)
		{
			success = null;
			nextStep = default(UriTemplateTrieNode.SingleLocationOrLocationsSet);
			if (wireUriSegments.Length <= currentLocation.node.depth)
			{
				if (currentLocation.node.endOfPath.Items.Count != 0)
				{
					success = currentLocation.node.endOfPath;
					return true;
				}
				if (currentLocation.node.star.Items.Count != 0)
				{
					success = currentLocation.node.star;
					return true;
				}
				nextStep = new UriTemplateTrieNode.SingleLocationOrLocationsSet(currentLocation.node.onFailure);
				return false;
			}
			else
			{
				UriTemplateLiteralPathSegment uriTemplateLiteralPathSegment = wireUriSegments[currentLocation.node.depth];
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				switch (currentLocation.locationWithin)
				{
				case UriTemplateTrieIntraNodeLocation.BeforeLiteral:
					flag = true;
					flag2 = true;
					flag3 = true;
					flag4 = true;
					break;
				case UriTemplateTrieIntraNodeLocation.AfterLiteral:
					flag = false;
					flag2 = true;
					flag3 = true;
					flag4 = true;
					break;
				case UriTemplateTrieIntraNodeLocation.AfterCompound:
					flag = false;
					flag2 = false;
					flag3 = true;
					flag4 = true;
					break;
				case UriTemplateTrieIntraNodeLocation.AfterVariable:
					flag = false;
					flag2 = false;
					flag3 = false;
					flag4 = true;
					break;
				}
				if (uriTemplateLiteralPathSegment.EndsWithSlash)
				{
					if (flag && currentLocation.node.nextLiteralSegment != null && currentLocation.node.nextLiteralSegment.ContainsKey(uriTemplateLiteralPathSegment))
					{
						nextStep = new UriTemplateTrieNode.SingleLocationOrLocationsSet(currentLocation.node.nextLiteralSegment[uriTemplateLiteralPathSegment]);
						return false;
					}
					IList<IList<UriTemplateTrieLocation>> locationsSet;
					if (flag2 && currentLocation.node.nextCompoundSegment != null && UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<UriTemplateTrieLocation>.Lookup(currentLocation.node.nextCompoundSegment, uriTemplateLiteralPathSegment, out locationsSet))
					{
						nextStep = new UriTemplateTrieNode.SingleLocationOrLocationsSet(locationsSet);
						return false;
					}
					if (flag3 && currentLocation.node.nextVariableSegment != null && !uriTemplateLiteralPathSegment.IsNullOrEmpty())
					{
						nextStep = new UriTemplateTrieNode.SingleLocationOrLocationsSet(currentLocation.node.nextVariableSegment);
						return false;
					}
					if (flag4 && currentLocation.node.star.Items.Count != 0)
					{
						success = currentLocation.node.star;
						return true;
					}
					nextStep = new UriTemplateTrieNode.SingleLocationOrLocationsSet(currentLocation.node.onFailure);
					return false;
				}
				else
				{
					if (flag && currentLocation.node.finalLiteralSegment != null && currentLocation.node.finalLiteralSegment.ContainsKey(uriTemplateLiteralPathSegment))
					{
						success = currentLocation.node.finalLiteralSegment[uriTemplateLiteralPathSegment];
						return true;
					}
					IList<IList<UriTemplatePathPartiallyEquivalentSet>> list;
					if (flag2 && currentLocation.node.finalCompoundSegment != null && UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<UriTemplatePathPartiallyEquivalentSet>.Lookup(currentLocation.node.finalCompoundSegment, uriTemplateLiteralPathSegment, out list))
					{
						if (list[0].Count == 1)
						{
							success = list[0][0];
						}
						else
						{
							success = new UriTemplatePathPartiallyEquivalentSet(currentLocation.node.depth + 1);
							for (int i = 0; i < list[0].Count; i++)
							{
								success.Items.AddRange(list[0][i].Items);
							}
						}
						return true;
					}
					if (flag3 && currentLocation.node.finalVariableSegment.Items.Count != 0)
					{
						success = currentLocation.node.finalVariableSegment;
						return true;
					}
					if (flag4 && currentLocation.node.star.Items.Count != 0)
					{
						success = currentLocation.node.star;
						return true;
					}
					nextStep = new UriTemplateTrieNode.SingleLocationOrLocationsSet(currentLocation.node.onFailure);
					return false;
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005F38 File Offset: 0x00004138
		private static UriTemplateTrieLocation GetFailureLocationFromLocationsSet(IList<IList<UriTemplateTrieLocation>> locationsSet)
		{
			return locationsSet[0][0].node.onFailure;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005F54 File Offset: 0x00004154
		private static void Validate(UriTemplateTrieNode root, bool allowDuplicateEquivalentUriTemplates)
		{
			Queue<UriTemplateTrieNode> queue = new Queue<UriTemplateTrieNode>();
			UriTemplateTrieNode uriTemplateTrieNode = root;
			IList<IList<UriTemplatePathPartiallyEquivalentSet>> values;
			int i;
			IList<IList<UriTemplateTrieLocation>> values2;
			int k;
			for (;;)
			{
				UriTemplateTrieNode.Validate(uriTemplateTrieNode.endOfPath, allowDuplicateEquivalentUriTemplates);
				UriTemplateTrieNode.Validate(uriTemplateTrieNode.finalVariableSegment, allowDuplicateEquivalentUriTemplates);
				UriTemplateTrieNode.Validate(uriTemplateTrieNode.star, allowDuplicateEquivalentUriTemplates);
				if (uriTemplateTrieNode.finalLiteralSegment != null)
				{
					foreach (KeyValuePair<UriTemplateLiteralPathSegment, UriTemplatePathPartiallyEquivalentSet> keyValuePair in uriTemplateTrieNode.finalLiteralSegment)
					{
						UriTemplateTrieNode.Validate(keyValuePair.Value, allowDuplicateEquivalentUriTemplates);
					}
				}
				if (uriTemplateTrieNode.finalCompoundSegment != null)
				{
					values = uriTemplateTrieNode.finalCompoundSegment.Values;
					for (i = 0; i < values.Count; i++)
					{
						if (!allowDuplicateEquivalentUriTemplates && values[i].Count > 1)
						{
							goto Block_5;
						}
						for (int j = 0; j < values[i].Count; j++)
						{
							UriTemplateTrieNode.Validate(values[i][j], allowDuplicateEquivalentUriTemplates);
						}
					}
				}
				if (uriTemplateTrieNode.nextLiteralSegment != null)
				{
					foreach (KeyValuePair<UriTemplateLiteralPathSegment, UriTemplateTrieLocation> keyValuePair2 in uriTemplateTrieNode.nextLiteralSegment)
					{
						queue.Enqueue(keyValuePair2.Value.node);
					}
				}
				if (uriTemplateTrieNode.nextCompoundSegment != null)
				{
					values2 = uriTemplateTrieNode.nextCompoundSegment.Values;
					for (k = 0; k < values2.Count; k++)
					{
						if (!allowDuplicateEquivalentUriTemplates && values2[k].Count > 1)
						{
							goto Block_11;
						}
						for (int l = 0; l < values2[k].Count; l++)
						{
							UriTemplateTrieLocation uriTemplateTrieLocation = values2[k][l];
							queue.Enqueue(uriTemplateTrieLocation.node);
						}
					}
				}
				if (uriTemplateTrieNode.nextVariableSegment != null)
				{
					queue.Enqueue(uriTemplateTrieNode.nextVariableSegment.node);
				}
				if (queue.Count == 0)
				{
					return;
				}
				uriTemplateTrieNode = queue.Dequeue();
			}
			Block_5:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTDuplicate", new object[]
			{
				values[i][0].Items[0].Key.ToString(),
				values[i][1].Items[0].Key.ToString()
			})));
			Block_11:
			UriTemplate uriTemplate = UriTemplateTrieNode.FindAnyUriTemplate(values2[k][0].node);
			UriTemplate uriTemplate2 = UriTemplateTrieNode.FindAnyUriTemplate(values2[k][1].node);
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTDuplicate", new object[]
			{
				uriTemplate.ToString(),
				uriTemplate2.ToString()
			})));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006248 File Offset: 0x00004448
		private static void Validate(UriTemplatePathPartiallyEquivalentSet pes, bool allowDuplicateEquivalentUriTemplates)
		{
			if (pes.Items.Count < 2)
			{
				return;
			}
			for (int i = 0; i < pes.Items.Count - 1; i++)
			{
			}
			UriTemplate[] array = new UriTemplate[pes.Items.Count];
			int num = 0;
			foreach (KeyValuePair<UriTemplate, object> keyValuePair in pes.Items)
			{
				if (pes.SegmentsCount >= keyValuePair.Key.segments.Count)
				{
					array[num++] = keyValuePair.Key;
				}
			}
			if (num > 0)
			{
				UriTemplateHelpers.DisambiguateSamePath(array, 0, num, allowDuplicateEquivalentUriTemplates);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006304 File Offset: 0x00004504
		private static UriTemplate FindAnyUriTemplate(UriTemplateTrieNode node)
		{
			while (node != null)
			{
				if (node.endOfPath.Items.Count > 0)
				{
					return node.endOfPath.Items[0].Key;
				}
				if (node.finalVariableSegment.Items.Count > 0)
				{
					return node.finalVariableSegment.Items[0].Key;
				}
				if (node.star.Items.Count > 0)
				{
					return node.star.Items[0].Key;
				}
				if (node.finalLiteralSegment != null)
				{
					UriTemplatePathPartiallyEquivalentSet anyDictionaryValue = UriTemplateTrieNode.GetAnyDictionaryValue<UriTemplatePathPartiallyEquivalentSet>(node.finalLiteralSegment);
					return anyDictionaryValue.Items[0].Key;
				}
				if (node.finalCompoundSegment != null)
				{
					UriTemplatePathPartiallyEquivalentSet anyValue = node.finalCompoundSegment.GetAnyValue();
					return anyValue.Items[0].Key;
				}
				if (node.nextLiteralSegment != null)
				{
					UriTemplateTrieLocation anyDictionaryValue2 = UriTemplateTrieNode.GetAnyDictionaryValue<UriTemplateTrieLocation>(node.nextLiteralSegment);
					node = anyDictionaryValue2.node;
				}
				else if (node.nextCompoundSegment != null)
				{
					UriTemplateTrieLocation anyValue2 = node.nextCompoundSegment.GetAnyValue();
					node = anyValue2.node;
				}
				else if (node.nextVariableSegment != null)
				{
					node = node.nextVariableSegment.node;
				}
				else
				{
					node = null;
				}
			}
			return null;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006450 File Offset: 0x00004650
		private static T GetAnyDictionaryValue<T>(IDictionary<UriTemplateLiteralPathSegment, T> dictionary)
		{
			T result;
			using (IEnumerator<T> enumerator = dictionary.Values.GetEnumerator())
			{
				enumerator.MoveNext();
				result = enumerator.Current;
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006494 File Offset: 0x00004694
		private void AddFinalCompoundSegment(UriTemplateCompoundPathSegment cps, KeyValuePair<UriTemplate, object> kvp)
		{
			if (this.finalCompoundSegment == null)
			{
				this.finalCompoundSegment = new UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<UriTemplatePathPartiallyEquivalentSet>();
			}
			UriTemplatePathPartiallyEquivalentSet uriTemplatePathPartiallyEquivalentSet = this.finalCompoundSegment.Find(cps);
			if (uriTemplatePathPartiallyEquivalentSet == null)
			{
				uriTemplatePathPartiallyEquivalentSet = new UriTemplatePathPartiallyEquivalentSet(this.depth + 1);
				this.finalCompoundSegment.Add(cps, uriTemplatePathPartiallyEquivalentSet);
			}
			uriTemplatePathPartiallyEquivalentSet.Items.Add(kvp);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000064EC File Offset: 0x000046EC
		private void AddFinalLiteralSegment(UriTemplateLiteralPathSegment lps, KeyValuePair<UriTemplate, object> kvp)
		{
			if (this.finalLiteralSegment != null && this.finalLiteralSegment.ContainsKey(lps))
			{
				this.finalLiteralSegment[lps].Items.Add(kvp);
				return;
			}
			if (this.finalLiteralSegment == null)
			{
				this.finalLiteralSegment = new Dictionary<UriTemplateLiteralPathSegment, UriTemplatePathPartiallyEquivalentSet>();
			}
			UriTemplatePathPartiallyEquivalentSet uriTemplatePathPartiallyEquivalentSet = new UriTemplatePathPartiallyEquivalentSet(this.depth + 1);
			uriTemplatePathPartiallyEquivalentSet.Items.Add(kvp);
			this.finalLiteralSegment.Add(lps, uriTemplatePathPartiallyEquivalentSet);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006564 File Offset: 0x00004764
		private UriTemplateTrieNode AddNextCompoundSegment(UriTemplateCompoundPathSegment cps)
		{
			if (this.nextCompoundSegment == null)
			{
				this.nextCompoundSegment = new UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<UriTemplateTrieLocation>();
			}
			UriTemplateTrieLocation uriTemplateTrieLocation = this.nextCompoundSegment.Find(cps);
			if (uriTemplateTrieLocation == null)
			{
				uriTemplateTrieLocation = new UriTemplateTrieLocation(new UriTemplateTrieNode(this.depth + 1)
				{
					onFailure = new UriTemplateTrieLocation(this, UriTemplateTrieIntraNodeLocation.AfterCompound)
				}, UriTemplateTrieIntraNodeLocation.BeforeLiteral);
				this.nextCompoundSegment.Add(cps, uriTemplateTrieLocation);
			}
			return uriTemplateTrieLocation.node;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000065CC File Offset: 0x000047CC
		private UriTemplateTrieNode AddNextLiteralSegment(UriTemplateLiteralPathSegment lps)
		{
			if (this.nextLiteralSegment != null && this.nextLiteralSegment.ContainsKey(lps))
			{
				return this.nextLiteralSegment[lps].node;
			}
			if (this.nextLiteralSegment == null)
			{
				this.nextLiteralSegment = new Dictionary<UriTemplateLiteralPathSegment, UriTemplateTrieLocation>();
			}
			UriTemplateTrieNode uriTemplateTrieNode = new UriTemplateTrieNode(this.depth + 1);
			uriTemplateTrieNode.onFailure = new UriTemplateTrieLocation(this, UriTemplateTrieIntraNodeLocation.AfterLiteral);
			this.nextLiteralSegment.Add(lps, new UriTemplateTrieLocation(uriTemplateTrieNode, UriTemplateTrieIntraNodeLocation.BeforeLiteral));
			return uriTemplateTrieNode;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006644 File Offset: 0x00004844
		private UriTemplateTrieNode AddNextVariableSegment()
		{
			if (this.nextVariableSegment != null)
			{
				return this.nextVariableSegment.node;
			}
			UriTemplateTrieNode uriTemplateTrieNode = new UriTemplateTrieNode(this.depth + 1);
			uriTemplateTrieNode.onFailure = new UriTemplateTrieLocation(this, UriTemplateTrieIntraNodeLocation.AfterVariable);
			this.nextVariableSegment = new UriTemplateTrieLocation(uriTemplateTrieNode, UriTemplateTrieIntraNodeLocation.BeforeLiteral);
			return uriTemplateTrieNode;
		}

		// Token: 0x04000092 RID: 146
		private int depth;

		// Token: 0x04000093 RID: 147
		private UriTemplatePathPartiallyEquivalentSet endOfPath;

		// Token: 0x04000094 RID: 148
		private UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<UriTemplatePathPartiallyEquivalentSet> finalCompoundSegment;

		// Token: 0x04000095 RID: 149
		private Dictionary<UriTemplateLiteralPathSegment, UriTemplatePathPartiallyEquivalentSet> finalLiteralSegment;

		// Token: 0x04000096 RID: 150
		private UriTemplatePathPartiallyEquivalentSet finalVariableSegment;

		// Token: 0x04000097 RID: 151
		private UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<UriTemplateTrieLocation> nextCompoundSegment;

		// Token: 0x04000098 RID: 152
		private Dictionary<UriTemplateLiteralPathSegment, UriTemplateTrieLocation> nextLiteralSegment;

		// Token: 0x04000099 RID: 153
		private UriTemplateTrieLocation nextVariableSegment;

		// Token: 0x0400009A RID: 154
		private UriTemplateTrieLocation onFailure;

		// Token: 0x0400009B RID: 155
		private UriTemplatePathPartiallyEquivalentSet star;

		// Token: 0x02000ABC RID: 2748
		private struct SingleLocationOrLocationsSet
		{
			// Token: 0x06006E0C RID: 28172 RVA: 0x0019B3ED File Offset: 0x001995ED
			public SingleLocationOrLocationsSet(UriTemplateTrieLocation singleLocation)
			{
				this.isSingle = true;
				this.singleLocation = singleLocation;
				this.locationsSet = null;
			}

			// Token: 0x06006E0D RID: 28173 RVA: 0x0019B404 File Offset: 0x00199604
			public SingleLocationOrLocationsSet(IList<IList<UriTemplateTrieLocation>> locationsSet)
			{
				this.isSingle = false;
				this.singleLocation = null;
				this.locationsSet = locationsSet;
			}

			// Token: 0x170019AE RID: 6574
			// (get) Token: 0x06006E0E RID: 28174 RVA: 0x0019B41B File Offset: 0x0019961B
			public bool IsSingle
			{
				get
				{
					return this.isSingle;
				}
			}

			// Token: 0x170019AF RID: 6575
			// (get) Token: 0x06006E0F RID: 28175 RVA: 0x0019B423 File Offset: 0x00199623
			public IList<IList<UriTemplateTrieLocation>> LocationsSet
			{
				get
				{
					return this.locationsSet;
				}
			}

			// Token: 0x170019B0 RID: 6576
			// (get) Token: 0x06006E10 RID: 28176 RVA: 0x0019B42B File Offset: 0x0019962B
			public UriTemplateTrieLocation SingleLocation
			{
				get
				{
					return this.singleLocation;
				}
			}

			// Token: 0x04003EEF RID: 16111
			private readonly bool isSingle;

			// Token: 0x04003EF0 RID: 16112
			private readonly IList<IList<UriTemplateTrieLocation>> locationsSet;

			// Token: 0x04003EF1 RID: 16113
			private readonly UriTemplateTrieLocation singleLocation;
		}

		// Token: 0x02000ABD RID: 2749
		private class AscendingSortedCompoundSegmentsCollection<T> where T : class
		{
			// Token: 0x06006E11 RID: 28177 RVA: 0x0019B433 File Offset: 0x00199633
			public AscendingSortedCompoundSegmentsCollection()
			{
				this.items = new SortedList<UriTemplateCompoundPathSegment, Collection<UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem>>();
			}

			// Token: 0x170019B1 RID: 6577
			// (get) Token: 0x06006E12 RID: 28178 RVA: 0x0019B448 File Offset: 0x00199648
			public IList<IList<T>> Values
			{
				get
				{
					IList<IList<T>> list = new List<IList<T>>(this.items.Count);
					for (int i = 0; i < this.items.Values.Count; i++)
					{
						list.Add(new List<T>(this.items.Values[i].Count));
						for (int j = 0; j < this.items.Values[i].Count; j++)
						{
							list[i].Add(this.items.Values[i][j].Value);
						}
					}
					return list;
				}
			}

			// Token: 0x06006E13 RID: 28179 RVA: 0x0019B4F4 File Offset: 0x001996F4
			public void Add(UriTemplateCompoundPathSegment segment, T value)
			{
				int num = this.items.IndexOfKey(segment);
				if (num == -1)
				{
					Collection<UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem> collection = new Collection<UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem>();
					collection.Add(new UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem(segment, value));
					this.items.Add(segment, collection);
					return;
				}
				Collection<UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem> collection2 = this.items.Values[num];
				collection2.Add(new UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem(segment, value));
			}

			// Token: 0x06006E14 RID: 28180 RVA: 0x0019B554 File Offset: 0x00199754
			public T Find(UriTemplateCompoundPathSegment segment)
			{
				int num = this.items.IndexOfKey(segment);
				if (num == -1)
				{
					return default(T);
				}
				Collection<UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem> collection = this.items.Values[num];
				for (int i = 0; i < collection.Count; i++)
				{
					if (collection[i].Segment.IsEquivalentTo(segment, false))
					{
						return collection[i].Value;
					}
				}
				return default(T);
			}

			// Token: 0x06006E15 RID: 28181 RVA: 0x0019B5D4 File Offset: 0x001997D4
			public IList<IList<T>> Find(UriTemplateLiteralPathSegment wireData)
			{
				IList<IList<T>> list = new List<IList<T>>();
				for (int i = 0; i < this.items.Values.Count; i++)
				{
					List<T> list2 = null;
					for (int j = 0; j < this.items.Values[i].Count; j++)
					{
						if (this.items.Values[i][j].Segment.IsMatch(wireData))
						{
							if (list2 == null)
							{
								list2 = new List<T>();
							}
							list2.Add(this.items.Values[i][j].Value);
						}
					}
					if (list2 != null)
					{
						list.Add(list2);
					}
				}
				return list;
			}

			// Token: 0x06006E16 RID: 28182 RVA: 0x0019B690 File Offset: 0x00199890
			public T GetAnyValue()
			{
				if (this.items.Values.Count > 0)
				{
					return this.items.Values[0][0].Value;
				}
				return default(T);
			}

			// Token: 0x06006E17 RID: 28183 RVA: 0x0019B6D9 File Offset: 0x001998D9
			public static bool Lookup(UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T> collection, UriTemplateLiteralPathSegment wireData, out IList<IList<T>> results)
			{
				results = collection.Find(wireData);
				return results != null && results.Count > 0;
			}

			// Token: 0x04003EF2 RID: 16114
			private SortedList<UriTemplateCompoundPathSegment, Collection<UriTemplateTrieNode.AscendingSortedCompoundSegmentsCollection<T>.CollectionItem>> items;

			// Token: 0x02000ED3 RID: 3795
			private struct CollectionItem
			{
				// Token: 0x0600847B RID: 33915 RVA: 0x001E99E0 File Offset: 0x001E7BE0
				public CollectionItem(UriTemplateCompoundPathSegment segment, T value)
				{
					this.segment = segment;
					this.value = value;
				}

				// Token: 0x17001D2C RID: 7468
				// (get) Token: 0x0600847C RID: 33916 RVA: 0x001E99F0 File Offset: 0x001E7BF0
				public UriTemplateCompoundPathSegment Segment
				{
					get
					{
						return this.segment;
					}
				}

				// Token: 0x17001D2D RID: 7469
				// (get) Token: 0x0600847D RID: 33917 RVA: 0x001E99F8 File Offset: 0x001E7BF8
				public T Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x04004CB5 RID: 19637
				private UriTemplateCompoundPathSegment segment;

				// Token: 0x04004CB6 RID: 19638
				private T value;
			}
		}
	}
}
