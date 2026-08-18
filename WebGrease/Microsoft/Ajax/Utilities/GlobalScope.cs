using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000097 RID: 151
	public sealed class GlobalScope : ActivationObject
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00029544 File Offset: 0x00027744
		public ICollection<UndefinedReference> UndefinedReferences
		{
			get
			{
				return this.m_undefined;
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002954C File Offset: 0x0002774C
		internal GlobalScope(CodeSettings settings) : base(null, settings)
		{
			base.ScopeType = ScopeType.Global;
			this.m_globalProperties = new HashSet<string>
			{
				"DOMParser",
				"Image",
				"Infinity",
				"JSON",
				"Math",
				"NaN",
				"System",
				"Windows",
				"WinJS",
				"XMLHttpRequest",
				"applicationCache",
				"clientInformation",
				"clipboardData",
				"closed",
				"console",
				"defaultStatus",
				"devicePixelRatio",
				"document",
				"event",
				"external",
				"frameElement",
				"frames",
				"history",
				"indexedDB",
				"innerHeight",
				"innerWidth",
				"length",
				"localStorage",
				"location",
				"name",
				"navigator",
				"offscreenBuffering",
				"opener",
				"outerHeight",
				"outerWidth",
				"pageXOffset",
				"pageYOffset",
				"parent",
				"screen",
				"screenLeft",
				"screenTop",
				"screenX",
				"screenY",
				"self",
				"sessionStorage",
				"status",
				"top",
				"undefined",
				"window"
			};
			this.m_globalFunctions = new HashSet<string>
			{
				"ActiveXObject",
				"Array",
				"ArrayBuffer",
				"ArrayBufferView",
				"Boolean",
				"DataView",
				"Date",
				"Debug",
				"Error",
				"EvalError",
				"EventSource",
				"File",
				"FileList",
				"FileReader",
				"Float32Array",
				"Float64Array",
				"Function",
				"Int16Array",
				"Int32Array",
				"Int8Array",
				"Iterator",
				"Map",
				"Node",
				"NodeFilter",
				"NodeIterator",
				"NodeList",
				"NodeSelector",
				"Number",
				"Object",
				"Proxy",
				"RangeError",
				"ReferenceError",
				"RegExp",
				"Set",
				"SharedWorker",
				"String",
				"SyntaxError",
				"TypeError",
				"Uint8Array",
				"Uint8ClampedArray",
				"Uint16Array",
				"Uint32Array",
				"URIError",
				"URL",
				"WeakMap",
				"WebSocket",
				"Worker",
				"addEventListener",
				"alert",
				"attachEvent",
				"blur",
				"cancelAnimationFrame",
				"captureEvents",
				"clearImmediate",
				"clearInterval",
				"clearTimeout",
				"close",
				"confirm",
				"createPopup",
				"decodeURI",
				"decodeURIComponent",
				"detachEvent",
				"dispatchEvent",
				"encodeURI",
				"encodeURIComponent",
				"escape",
				"eval",
				"execScript",
				"focus",
				"getComputedStyle",
				"getSelection",
				"importScripts",
				"isFinite",
				"isNaN",
				"matchMedia",
				"moveBy",
				"moveTo",
				"navigate",
				"open",
				"parseFloat",
				"parseInt",
				"postMessage",
				"prompt",
				"releaseEvents",
				"removeEventListener",
				"requestAnimationFrame",
				"resizeBy",
				"resizeTo",
				"scroll",
				"scrollBy",
				"scrollTo",
				"setActive",
				"setImmediate",
				"setInterval",
				"setTimeout",
				"showModalDialog",
				"showModelessDialog",
				"styleMedia",
				"unescape"
			};
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00029C72 File Offset: 0x00027E72
		public void AddUndefinedReference(UndefinedReference exception)
		{
			if (this.m_undefined == null)
			{
				this.m_undefined = new HashSet<UndefinedReference>();
			}
			this.m_undefined.Add(exception);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00029C94 File Offset: 0x00027E94
		internal void SetAssumedGlobals(CodeSettings settings)
		{
			if (settings != null)
			{
				this.m_assumedGlobals = ((settings.KnownGlobalCollection == null) ? new HashSet<string>() : new HashSet<string>(settings.KnownGlobalCollection));
				foreach (string text in settings.DebugLookupCollection)
				{
					this.m_assumedGlobals.Add(text.SubstringUpToFirst('.'));
				}
				using (IEnumerator<ResourceStrings> enumerator2 = settings.ResourceStrings.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ResourceStrings resourceStrings = enumerator2.Current;
						if (!resourceStrings.Name.IsNullOrWhiteSpace())
						{
							this.m_assumedGlobals.Add(resourceStrings.Name.SubstringUpToFirst('.'));
						}
					}
					return;
				}
			}
			this.m_assumedGlobals = new HashSet<string>();
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00029D7C File Offset: 0x00027F7C
		internal override void AnalyzeScope()
		{
			base.ManualRenameFields();
			foreach (ActivationObject activationObject in base.ChildScopes)
			{
				if (!activationObject.Existing)
				{
					activationObject.AnalyzeScope();
				}
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00029DD8 File Offset: 0x00027FD8
		internal override void AutoRenameFields()
		{
			foreach (ActivationObject activationObject in base.ChildScopes)
			{
				if (!activationObject.Existing)
				{
					activationObject.AutoRenameFields();
				}
			}
		}

		// Token: 0x17000237 RID: 567
		public override JSVariableField this[string name]
		{
			get
			{
				JSVariableField jsvariableField = base[name];
				if (jsvariableField == null)
				{
					jsvariableField = this.ResolveFromCollection(name, this.m_globalProperties, FieldType.Predefined, false);
				}
				if (jsvariableField == null)
				{
					jsvariableField = this.ResolveFromCollection(name, this.m_globalFunctions, FieldType.Predefined, true);
				}
				if (jsvariableField == null)
				{
					jsvariableField = this.ResolveFromCollection(name, this.m_assumedGlobals, FieldType.Global, false);
				}
				if (jsvariableField == null && GlobalScope.s_blanketPrefixes.IsMatch(name))
				{
					jsvariableField = new JSVariableField(FieldType.Predefined, name, FieldAttributes.PrivateScope, null);
					base.AddField(jsvariableField);
				}
				return jsvariableField;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00029EA0 File Offset: 0x000280A0
		private JSVariableField ResolveFromCollection(string name, HashSet<string> collection, FieldType fieldType, bool isFunction)
		{
			if (collection.Contains(name))
			{
				return base.AddField(new JSVariableField(fieldType, name, FieldAttributes.PrivateScope, null)
				{
					IsFunction = isFunction
				});
			}
			return null;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00029ED1 File Offset: 0x000280D1
		public override void DeclareScope()
		{
			base.DefineLexicalDeclarations();
			base.DefineVarDeclarations();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00029EDF File Offset: 0x000280DF
		public override JSVariableField CreateField(string name, object value, FieldAttributes attributes)
		{
			return new JSVariableField(FieldType.Global, name, attributes, value);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00029EEA File Offset: 0x000280EA
		public override JSVariableField CreateField(JSVariableField outerField)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000342 RID: 834
		private static Regex s_blanketPrefixes = new Regex("^(?:ms|MS|o|webkit|moz|Gecko|HTML)(?:[A-Z][a-z0-9]*)+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000343 RID: 835
		private HashSet<string> m_globalProperties;

		// Token: 0x04000344 RID: 836
		private HashSet<string> m_globalFunctions;

		// Token: 0x04000345 RID: 837
		private HashSet<string> m_assumedGlobals;

		// Token: 0x04000346 RID: 838
		private HashSet<UndefinedReference> m_undefined;
	}
}
