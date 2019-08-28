﻿using System;
using System.Collections;
using UnityEngine;

namespace KoganeUnityLib
{
	/// <summary>
	/// コルーチンに関する汎用クラス
	/// </summary>
	public static class CoroutineUtils
	{
		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private static readonly MonoBehaviour _monoBehaviour;

		private static bool IsInvalid => _monoBehaviour == null || _monoBehaviour.gameObject == null;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// コルーチンを管理するゲームオブジェクトを生成するコンストラクタ
		/// </summary>
		static CoroutineUtils()
		{
			var gameObject = new GameObject( "CoroutineUtils" );
			GameObject.DontDestroyOnLoad( gameObject );
			_monoBehaviour = gameObject.AddComponent<FooBehaviour>();
		}
		
		//==============================================================================
		// CallWaitForCondition
		//==============================================================================
		/// <summary>
		/// 条件を満たしたら Action デリゲートを呼び出します
		/// </summary>
		public static void CallWaitForCondition( GameObject gameObject, Func<bool> condition, Action callback )
		{
			if ( IsInvalid ) return;

			if ( condition() )
			{
				callback?.Invoke();
				return;
			}

			_monoBehaviour.StartCoroutine( DoCallWaitForCondition( gameObject, condition, callback ) );
		}

		/// <summary>
		/// 条件を満たしたら Action デリゲートを呼び出します
		/// </summary>
		public static IEnumerator DoCallWaitForCondition( GameObject gameObject, Func<bool> condition, Action callback )
		{
			while ( !condition() ) yield return 0;
			if ( gameObject == null ) yield break;
			callback?.Invoke();
		}

		/// <summary>
		/// 条件を満たしたら Action デリゲートを呼び出します
		/// </summary>
		public static void CallWaitForCondition( Func<bool> condition, Action callback )
		{
			if ( IsInvalid ) return;

			if ( condition() )
			{
				callback?.Invoke();
				return;
			}

			_monoBehaviour.StartCoroutine( DoCallWaitForCondition( condition, callback ) );
		}

		/// <summary>
		/// 条件を満たしたら Action デリゲートを呼び出します
		/// </summary>
		public static IEnumerator DoCallWaitForCondition( Func<bool> condition, Action callback )
		{
			while ( !condition() ) yield return 0;
			callback?.Invoke();
		}
		
		//==============================================================================
		// CallWaitForEndOfFrame
		//==============================================================================
		/// <summary>
		/// 1 フレーム待機してから Action デリゲートを呼び出します
		/// </summary>
		public static void CallWaitForEndOfFrame( GameObject gameObject, Action callback )
		{
			if ( IsInvalid ) return;

			_monoBehaviour.StartCoroutine( DoCallWaitForEndOfFrame( gameObject, callback ) );
		}

		/// <summary>
		/// 1 フレーム待機してから Action デリゲートを呼び出します
		/// </summary>
		private static IEnumerator DoCallWaitForEndOfFrame( GameObject gameObject, Action callback )
		{
			yield return 0;
			if ( gameObject == null ) yield break;
			callback?.Invoke();
		}
		
		/// <summary>
		/// 1 フレーム待機してから Action デリゲートを呼び出します
		/// </summary>
		public static void CallWaitForEndOfFrame( Action callback )
		{
			if ( IsInvalid ) return;

			_monoBehaviour.StartCoroutine( DoCallWaitForEndOfFrame( callback ) );
		}

		/// <summary>
		/// 1 フレーム待機してから Action デリゲートを呼び出します
		/// </summary>
		private static IEnumerator DoCallWaitForEndOfFrame( Action callback )
		{
			yield return 0;
			callback?.Invoke();
		}
		
		//==============================================================================
		// CallWaitForSeconds
		//==============================================================================
		/// <summary>
		/// 指定された秒数待機してから Action デリゲートを呼び出します
		/// </summary>
		public static void CallWaitForSeconds( GameObject gameObject, float seconds, Action callback )
		{
			if ( IsInvalid ) return;
			
			if ( seconds <= 0 )
			{
				callback?.Invoke();
				return;
			}

			_monoBehaviour.StartCoroutine( DoCallWaitForSeconds( gameObject, seconds, callback ) );
		}

		/// <summary>
		/// 指定された秒数待機してから Action デリゲートを呼び出します
		/// </summary>
		private static IEnumerator DoCallWaitForSeconds( GameObject gameObject, float seconds, Action callback )
		{
			yield return new WaitForSeconds( seconds );
			if ( gameObject == null ) yield break;
			callback?.Invoke();
		}

		/// <summary>
		/// 指定された秒数待機してから Action デリゲートを呼び出します
		/// </summary>
		public static void CallWaitForSeconds( float seconds, Action callback )
		{
			if ( IsInvalid ) return;
			
			if ( seconds <= 0 )
			{
				callback?.Invoke();
				return;
			}

			_monoBehaviour.StartCoroutine( DoCallWaitForSeconds( seconds, callback ) );
		}

		/// <summary>
		/// 指定された秒数待機してから Action デリゲートを呼び出します
		/// </summary>
		private static IEnumerator DoCallWaitForSeconds( float seconds, Action callback )
		{
			yield return new WaitForSeconds( seconds );
			callback?.Invoke();
		}
		
		//==============================================================================
		// StartCoroutine
		//==============================================================================
		public static void StartCoroutine( IEnumerator routice )
		{
			_monoBehaviour.StartCoroutine( routice );
		}
	}
}