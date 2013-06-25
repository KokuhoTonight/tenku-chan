using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Deque<T> : IEnumerable<T> {
	T[] array;
	int s, e, cnt;
	
	public Deque() : this(0) {
	}
	
	public Deque(IEnumerable<T> collection) : this(0) {
		foreach (T item in collection) {
			AddLast(item);
		}
	}
	
	public Deque(int capacity) {
		array = new T[capacity];
		s = 0;
		e = capacity - 1;
		cnt = 0;
	}
	             
	public T this[int index] {
		get {
			if (cnt <= 0 || index < 0 || cnt <= index) {
				throw new IndexOutOfRangeException();
			}
			return array[(s + index) % array.Length];
		}
	}
	
	public int count {
		get { return cnt; }
	}
	
	public int capacity {
		get { return array.Length; }
	}
	
	public T first {
		get {
			if (cnt <= 0) {
				throw new InvalidOperationException();
			}
			return array[s];
		}
	}
	
	public T last {
		get {
			if (cnt <= 0) {
				throw new InvalidOperationException();
			}
			return array[e];
		}
	}
	
	public IEnumerator<T> GetEnumerator() {
		for (int i = 0; i < cnt; ++i) {
			yield return array[(s + i) % array.Length];
		}
	}
	
	public Deque<T> GetRange(int index, int count) {
		if (0 <= index && index + count < cnt) {
			Deque<T> deque = new Deque<T>(count);
			for (int i = index; i < index + count; ++i) {
				deque.AddLast(array[(s + i) % array.Length]);
			}
			return deque;
		}
		throw new IndexOutOfRangeException();
	}
	
	public void ForEach(Action<T> action) {
		for (int i = 0; i < cnt; ++i) {
			action(array[(s + i) % array.Length]);
		}
	}
	
	public int IndexOf(T item) {
		for (int i = 0; i < cnt; ++i) {
			if (EqualityComparer<T>.Default.Equals(array[(s + i) % array.Length], item)) {
				return i;
			}
		}
		return -1;
	}
	
	public int LastIndexOf(T item) {
		for (int i = cnt - 1; 0 <= i; --i) {
			if (EqualityComparer<T>.Default.Equals(array[(s + i) % array.Length], item)) {
				return i;
			}
		}
		return -1;
	}
	
	public bool Exists(Predicate<T> match) {
		for (int i = 0; i < cnt; ++i) {
			if (match(array[(s + i) % array.Length])) {
				return true;
			}
		}
		return false;
	}
	
	public bool TrueForAll(Predicate<T> match) {
		for (int i = 0; i < cnt; ++i) {
			if (!match(array[(s + i) % array.Length])) {
				return false;
			}
		}
		return true;
	}
	
	public T Find(Predicate<T> match) {
		for (int i = 0; i < cnt; ++i) {
			T item = array[(s + i) % array.Length];
			if (match(item)) {
				return item;
			}
		}
		return default(T);
	}
	
	public T FindLast(Predicate<T> match) {
		for (int i = cnt - 1; 0 <= i; --i) {
			T item = array[(s + i) % array.Length];
			if (match(item)) {
				return item;
			}
		}
		return default(T);
	}
	
	public int FindIndex(Predicate<T> match) {
		return FindIndex(0, cnt, match);
	}
	
	public int FindIndex(int index, Predicate<T> match) {
		return FindIndex(index, cnt - index, match);
	}
	
	public int FindIndex(int index, int count, Predicate<T> match) {
		if (0 <= index && index + count < cnt) {
			for (int i = index; i < index + count; ++i) {
				T item = array[(s + i) % array.Length];
				if (match(item)) {
					return i;
				}
			}
		}
		return -1;
	}
	
	public int FindLastIndex(Predicate<T> match) {
		return FindLastIndex(0, cnt, match);
	}
	
	public int FindLastIndex(int index, Predicate<T> match) {
		return FindLastIndex(index, cnt - index, match);
	}
	
	public int FindLastIndex(int index, int count, Predicate<T> match) {
		if (0 <= index && index + count < cnt) {
			for (int i = index + count - 1; index <= i; ++i) {
				T item = array[(s + i) % array.Length];
				if (match(item)) {
					return i;
				}
			}
		}
		return -1;
	}
	
	public Deque<T> FindAll(Predicate<T> match) {
		Deque<T> deque = new Deque<T>();
		for (int i = 0; i < cnt; ++i) {
			T item = array[(s + i) % array.Length];
			if (match(item)) {
				deque.AddLast(item);
			}
		}
		return deque;
	}
	
	public Deque<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) {
		Deque<TOutput> deque = new Deque<TOutput>();
		ForEach(delegate(T obj) {
			deque.AddLast(converter(obj));
		});
		return deque;
	}
	
	public T[] ToArray() {
		return ToArray(cnt);
	}
	
	public void AddFirst(T value) {
		if (array.Length <= cnt) {
			int cap = cnt * 2;
			if (cap < 4) {
				cap = 4;
			}
			EnsureCapacity(cap);
		}
		--s;
		if (s < 0) {
			s = array.Length - 1;
		}
		array[s] = value;
		++cnt;
	}
	
	public void AddRangeFirst(IEnumerable<T> collection) {
		foreach (T item in collection) {
			AddFirst(item);
		}
	}
	
	public void AddLast(T value) {
		if (array.Length <= cnt) {
			int cap = cnt * 2;
			if (cap < 4) {
				cap = 4;
			}
			EnsureCapacity(cap);
		}
		++e;
		if (array.Length <= e) {
			e = 0;
		}
		array[e] = value;
		++cnt;
	}
	
	public void AddRangeLast(IEnumerable<T> collection) {
		foreach (T item in collection) {
			AddLast(item);
		}
	}
	
	public void RemoveFirst() {
		if (cnt <= 0) {
			throw new InvalidOperationException();
		}
		++s;
		if (array.Length <= s) {
			s = 0;
		}
		--cnt;
	}
	
	public void RemoveLast() {
		if (cnt <= 0) {
			throw new InvalidOperationException();
		}
		--e;
		if (e < 0) {
			e = array.Length - 1;
		}
		--cnt;
	}
	
	public void Add(T item) {
		AddLast(item);
	}
	
	public T Shift() {
		T item = first;
		RemoveFirst();
		return item;
	}
	
	public T Pop() {
		T item = last;
		RemoveLast();
		return item;
	}
	
	public Deque<T> Unshift(T item) {
		AddFirst(item);
		return this;
	}
	
	public Deque<T> Push(T item) {
		AddLast(item);
		return this;
	}
	
	public void Clear() {
		s = 0;
		e = capacity - 1;
		cnt = 0;
	}
	
	public void EnsureCapacity(int capacity) {
		if (array.Length < capacity) {
			array = ToArray(capacity);
			s = 0;
			e = cnt - 1;
		}
	}
	
	T[] ToArray(int length) {
		T[] newArray = new T[length];
		if (s < e) {
			Array.Copy(array, s, newArray, 0, e - s + 1);
		} else if (e < s) {
			Array.Copy(array, s, newArray, 0, array.Length - s);
			Array.Copy(array, 0, newArray, array.Length - s, e + 1);
		} else if (1 < cnt) {
			Array.Copy(array, s, newArray, 0, array.Length - s);
			Array.Copy(array, 0, newArray, array.Length - s, s);
		} else if (0 < cnt) {
			newArray[0] = array[s];
		}
		return newArray;
	}
	
	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}
}
