using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

public class JSONObject {
	object o;
	
	public bool ContainsKey(string key) {
		Dictionary<string, JSONObject> d = o as Dictionary<string, JSONObject>;
		return d != null && d.ContainsKey(key);
	}
	
	public JSONObject() {
		o = null;
	}
	
	public JSONObject(object obj) {
		o = obj;
	}
	
	public JSONObject this[string key] {
		get {
			Dictionary<string, JSONObject> dictionary;
			if (o == null) {
				o = dictionary = new Dictionary<string, JSONObject>();
			} else {
				dictionary = o as Dictionary<string, JSONObject>;
			}
			if (dictionary != null) {
				JSONObject result;
				if (dictionary.TryGetValue(key, out result)) {
					return result;
				} else {
					return dictionary[key] = new JSONObject();
				}
			} else {
				Debug.LogWarning("JSON object error.");
				return null;
			}
		}
		set {
			Dictionary<string, JSONObject> dictionary;
			if (o == null) {
				o = dictionary = new Dictionary<string, JSONObject>();
			} else {
				dictionary = o as Dictionary<string, JSONObject>;
			}
			if (dictionary != null) {
				dictionary[key] = value;
			} else {
				Debug.LogWarning("JSON object error.");
			}
		}
	}
	
	public JSONObject this[int index] {
		get {
			List<JSONObject> list;
			if (o == null) {
				o = list = new List<JSONObject>();
			} else {
				list = o as List<JSONObject>;
			}
			if (list != null) {
				if (index < list.Count) {
					return ((List<JSONObject>)o)[index];
				} else {
					while (list.Count <= index) {
						list.Add(new JSONObject());
					}
					return list[index];
				}
			} else {
				Debug.LogWarning("JSON object error.");
				return null;
			}
		}
		set {
			List<JSONObject> list;
			if (o == null) {
				o = list = new List<JSONObject>();
			} else {
				list = o as List<JSONObject>;
			}
			if (list != null) {
				if (index < list.Count) {
					list[index] = value;
				} else {
					while (list.Count <= index - 1) {
						list.Add(new JSONObject());
					}
					list.Add(value);
				}
			} else {
				Debug.LogWarning("JSON object error.");
			}
		}
	}
	
	public Dictionary<string, JSONObject> dictionaryOf {
		get {
			if (o is Dictionary<string, JSONObject>) {
				return (Dictionary<string, JSONObject>)o;
			} else {
				return new Dictionary<string, JSONObject>();
			}
		}
	}
	
	public List<JSONObject> listOf {
		get {
			if (o is List<JSONObject>) {
				return (List<JSONObject>)o;
			} else {
				return new List<JSONObject>();
			}
		}
	}
	
	public bool isNull {
		get { return o == null; }
	}
	
	public bool boolValue {
		get {
			if (o is bool) {
				return (bool)o;
			} else {
				Debug.LogWarning("JSON object error.");
				return false;
			}
		}
	}
	
	public int intValue {
		get {
			if (o is int) {
				return (int)o;
			} else {
				Debug.LogWarning("JSON object error.");
				return 0;
			}
		}
	}
	
	public float floatValue {
		get {
			if (o is float) {
				return (float)o;
			} else if (o is int) {
				return (float)(int)o;
			} else {
				Debug.LogWarning("JSON object error.");
				return 0;
			}
		}
	}
	
	public string stringValue {
		get {
			if (o is string) {
				return (string)o;
			} else if (o is int || o is float || o is bool) {
				return o.ToString();
			} else if (o == null) {
				return "";
			} else {
				Debug.LogWarning("JSON object error.");
				return "";
			}
		}
	}
	
	public T[] ToArray<T>(IJSONParser<T> parser) {
		List<JSONObject> list = o as List<JSONObject>;
		if (list != null) {
			T[] array = new T[list.Count];
			for (int i = 0; i < list.Count; ++i) {
				array[i] = (T)parser.Parse(list[i]);
			}
			return array;
		} else {
			Debug.LogWarning("JSON object error.");
			return new T[0];
		}
	}
	
	public int[] ToIntArray() {
		return ToArray(new IntParser());
	}
	
	public float[] ToFloatArray() {
		return ToArray(new FloatParser());
	}
	
	public bool[] ToBoolArray() {
		return ToArray(new BoolParser());
	}
	
	public string[] ToStringArray() {
		return ToArray(new StringParser());
	}
	
	public void SetArray<T>(T[] array, IJSONParser<T> parser) {
		List<JSONObject> list;
		if (o == null) {
			o = list = new List<JSONObject>();
		} else {
			list = o as List<JSONObject>;
		}
		if (list != null) {
			list.Clear();
			foreach (T item in array) {
				list.Add(parser.Deparse(item));
			}
		} else {
			Debug.LogWarning("JSON object error.");
		}
	}
	
	public void SetBoolArray(bool[] array) {
		SetArray(array, new BoolParser());
	}
	
	public void SetIntArray(int[] array) {
		SetArray(array, new IntParser());
	}
	
	public void SetFloatArray(float[] array) {
		SetArray(array, new FloatParser());
	}
	
	public void SetStringArray(string[] array) {
		SetArray(array, new StringParser());
	}
	
	public override string ToString() {
		if (o is Dictionary<string, JSONObject>) {
			Dictionary<string, JSONObject> h = (Dictionary<string, JSONObject>)o;
			StringBuilder builder = new StringBuilder();
			builder.Append("{");
			IEnumerator<KeyValuePair<string, JSONObject>> e = h.GetEnumerator();
			for (int i = 0; i < h.Count - 1; ++i) {
				e.MoveNext();
				builder.Append("\"" + e.Current.Key + "\"");
				builder.Append(":");
				builder.Append(e.Current.Value);
				builder.Append(",");
			}
			if (0 < h.Count) {
				e.MoveNext();
				builder.Append("\"" + e.Current.Key + "\"");
				builder.Append(":");
				builder.Append(e.Current.Value);
			}
			builder.Append("}");
			return builder.ToString();
		} else if (o is List<JSONObject>) {
			List<JSONObject> a = (List<JSONObject>)o;
			StringBuilder builder = new StringBuilder();
			builder.Append("[");
			for (int i = 0; i < a.Count - 1; ++i) {
				builder.Append(a[i]);
				builder.Append(",");
			}
			if (0 < a.Count) {
				builder.Append(a[a.Count - 1]);
			}
			builder.Append("]");
			return builder.ToString();
		} else {
			if (o != null) {
				if (o is string) {
					return "\"" + (string)o + "\"";
				} else if (o is bool) {
					return (bool)o ? "true" : "false";
				} else {
					return o.ToString();
				}
			} else {
				return "null";
			}
		}
	}
	
	public static JSONObject Read(string text) {
		return new Parser(text).Parse();
	}
	
	class BoolParser : IJSONParser<bool> {
		public bool Parse(JSONObject json) {
			return json.boolValue;
		}
		
		public JSONObject Deparse(bool obj) {
			return new JSONObject(obj);
		}
	}
	
	class IntParser : IJSONParser<int> {
		public int Parse(JSONObject json) {
			return json.intValue;
		}
		
		public JSONObject Deparse(int obj) {
			return new JSONObject(obj);
		}
	}
	
	class FloatParser : IJSONParser<float> {
		public float Parse(JSONObject json) {
			return json.floatValue;
		}
		
		public JSONObject Deparse(float obj) {
			return new JSONObject(obj);
		}
	}
	
	class StringParser : IJSONParser<string> {
		public string Parse(JSONObject json) {
			return json.stringValue;
		}
		
		public JSONObject Deparse(string obj) {
			return new JSONObject(obj);
		}
	}
	
	class Parser {
		Deque<char> buf = new Deque<char>();
		
		public Parser(string s) {
			buf.AddRangeLast(s);
		}
		
		public JSONObject Parse() {
			object result;
			switch (Read(out result)) {
			case Token.Null:
			case Token.True:
			case Token.False:
			case Token.Number:
			case Token.String:
			case Token.Array:
			case Token.Object:
				Trim();
				return (buf.count <= 0) ? new JSONObject(result) : null;
			default:
				return null;
			}
		}
		
		Token Read(out object result) {
			Trim();
			if (Match(',')) { // Value separator.
				result = null;
				return Token.ValueSeparator;
			} else if (Match(':')) { // Name separator.
				result = null;
				return Token.NameSeparator;
			} else if (Match('{')) { // Object.
				Dictionary<string, JSONObject> dictionary = new Dictionary<string, JSONObject>();
				while (true) {
					string name = null;
					object obj;
					switch (Read(out obj)) { // Name.
					case Token.EndOfObject:
						if (dictionary.Count <= 0) { // Empty object.
							goto Object;
						} else {
							throw new JSONException();
						}
					case Token.String:
						name = (string)obj;
						break;
					default:
						throw new JSONException();
					}
					switch (Read(out obj)) { // Name separator.
					case Token.NameSeparator:
						break;
					default:
						throw new JSONException();
					}
					switch (Read(out obj)) { // Value.
					case Token.Null:
					case Token.True:
					case Token.False:
					case Token.Number:
					case Token.String:
					case Token.Array:
					case Token.Object:
						dictionary.Add(name, new JSONObject(obj));
						break;
					default:
						throw new JSONException();
					}
					switch (Read(out obj)) {
					case Token.EndOfObject: // End of object.
						goto Object;
					case Token.ValueSeparator: // Has next member.
						continue;
					default:
						throw new JSONException();
					}
				}
			Object:
				result = dictionary;
				return Token.Object;
			} else if (Match('}')) {
				result = null;
				return Token.EndOfObject;
			} else if (Match('[')) { // Array.
				List<JSONObject> list = new List<JSONObject>();
				while (true) {
					object obj;
					switch (Read(out obj)) { // Element.
					case Token.EndOfArray:
						if (list.Count <= 0) { // Empty array.
							goto Array;
						} else {
							throw new JSONException();
						}
					case Token.Null:
					case Token.True:
					case Token.False:
					case Token.Number:
					case Token.String:
					case Token.Array:
					case Token.Object:
						list.Add(new JSONObject(obj));
						break;
					default:
						throw new JSONException();
					}
					switch (Read(out obj)) {
					case Token.EndOfArray: // End of array.
						goto Array;
					case Token.ValueSeparator: // Has next element.
						continue;
					default:
						throw new JSONException();
					}
				}
			Array:
				result = list;
				return Token.Array;
			} else if (Match(']')) {
				result = null;
				return Token.EndOfArray;
			} else if (Match('"')) { // String.
				bool escape = false;
				StringBuilder sb = new StringBuilder();
				while (0 < buf.count) {
					if (escape) {
						char c;
						/*if (MatchAny("\"\\/bfnrt", out c)) {
							sb.Append(c);
							escape = false;*/
						if (Match('"')) {
							sb.Append('"');escape = false;
							escape = false;
						} else if (Match('\\')) {
							sb.Append('\\');
							escape = false;
						} else if (Match('\b')) {
							sb.Append('\b');
							escape = false;
						} else if (Match('\f')) {
							sb.Append('\f');
							escape = false;
						} else if (Match('\n')) {
							sb.Append('\n');
							escape = false;
						} else if (Match('\r')) {
							sb.Append('\r');
							escape = false;
						} else if (Match('\t')) {
							sb.Append('\t');
							escape = false;
						} else if (Match('u')) {
							sb.Append('u');
							for (int i = 0; i < 4; ++i) {
								if (MatchAny("0123456789abcdefABCDEF", out c)) {
									sb.Append(c);
								} else {
									throw new JSONException();
								}
							}
							escape = false;
						} else {
							throw new JSONException();
						}
					} else {
						if (Match('\\')) {
//							sb.Append('\\');
							escape = true;
						} else if (Match('"')) {
							break;
						} else if (0 < buf.count) {
							sb.Append(buf.first);
//							buf.Remove(0, 1);
							buf.RemoveFirst();
						} else {
							throw new JSONException();
						}
					}
				}
				result = sb.ToString();
				return Token.String;
			} else if (MatchAll("true")) { // True.
				result = true;
				return Token.True;
			} else if (MatchAll("false")) { // False.
				result = false;
				return Token.False;
			} else if (MatchAll("null")) { // Null.
				result = null;
				return Token.Null;
			} else { // Number.
				char c;
				int sign;
				if (Match('-')) {
					sign = -1;
				} else if (Match('+')) {
					sign = 1;
				} else {
					sign = 1;
				}
				long intPart = 0;
				double fracPart = 0;
				if (Match('0')) {
					if (Match('.')) {
						goto Fraction;
					} else { // Zero.
						result = 0; // 32 bit integer.
						return Token.Number;
					}
				} else if (MatchAny("123456789", out c)) {
					intPart = c - '0';
					while (MatchAny("1234567890", out c)) {
						intPart = (intPart * 10) + (c - '0');
					}
					if (Match('.')) {
						goto Fraction;
					} else {
						goto Integer;
					}
				}
				throw new JSONException();
			Integer:
				while (MatchAny("0123456789", out c)) {
					intPart = (intPart * 10) + (c - '0');
				}
				if (MatchAny("eE", out c)) {
					goto Exponent;
				}
				result = (int)(intPart * sign); // 32 bit integer.
				return Token.Number;
			Fraction:
				double d = 1;
				while (MatchAny("0123456789", out c)) {
					fracPart += (c - '0') * (d *= 0.1);
				}
				if (MatchAny("eE", out c)) {
					goto Exponent;
				}
				result = (float)(intPart + fracPart)*sign; // 32 bit floating value.
				return Token.Number;
			Exponent:
				int expSign;
				if (Match('+')) {
					expSign = 1;
				} else if (Match('-')) {
					expSign = -1;
				} else {
					expSign = 1;
				}
				int expPart = 0;
				if (MatchAny("123456789", out c)) {
					expPart = (expPart * 10) + (c - '0');
					while (MatchAny("0123456789", out c)) {
						expPart = (expPart * 10) + (c - '0');
					}
					result = (float)((intPart + fracPart) * Math.Pow(10, expPart * expSign)); // 32 bit floating value.
					return Token.Number;
				} else {
					throw new JSONException();
				}
			}
		}
		
		void Trim() {
//			int n = 0;
//			while (n < buf.count && char.IsWhiteSpace(buf[n])) {
//				++n;
//			}
//			buf.Remove(0, n);
			int n = 0;
			while (n < buf.count && char.IsWhiteSpace(buf.first)) {
				buf.RemoveFirst();
			}
		}
		
		bool Match(char value) {
			if (0 < buf.count && buf.first == value) {
//				buf.Remove(0, 1);
				buf.RemoveFirst();
				return true;
			} else {
				return false;
			}
		}
		
		bool MatchAny(string value, out char result) {
			if (0 < buf.count) {
				foreach (char c in value) {
					if (buf.first == c) {
						result = buf.first;
//						buf.Remove(0, 1);
						buf.RemoveFirst();
						return true;
					}
				}
			}
			result = '\0';
			return false;
		}
		
		bool MatchAll(string value) {
			int i = 0;
			while (i < buf.count && i < value.Length && buf[i] == value[i]) {
				++i;
			}
			if (i == value.Length) {
//				buf.Remove(0, value.Length);
				for (int j = 0; j < value.Length; ++j) {
					buf.RemoveFirst();
				}
				return true;
			}
			return false;
		}
		
		enum Token {
			Null,
			True,
			False,
			Number,
			String,
			Array,
			Object,
			EndOfArray,
			EndOfObject,
			ValueSeparator,
			NameSeparator
		}
	}
}

public interface IJSONParser<T> {
	T Parse(JSONObject json);
	
	JSONObject Deparse(T obj);
}

public class JSONException : Exception {
}
