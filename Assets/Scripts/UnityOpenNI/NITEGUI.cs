using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NITEGUI
{
	private static Dictionary<GUIStyle, GUIStyle> activeStyles;
	private static Dictionary<GUIStyle, GUIStyle> hoverStyles;
	public static NITECursor NiteCursor;
	public static bool ClickOnRelease;
	
	public static bool Button(Rect position, string txt)
	{
		return Button(position, txt, GUI.skin.button);
	}
	
	public static bool Button(Rect position, string txt, GUIStyle guiStyle)
	{
		bool bMouseOver = IsCursorOver(position);
		bool bClick = IsClickOver(position);

		// choose styles based on hover/click
		if (bMouseOver)
		{
			guiStyle = getHoverGuiStyle(guiStyle);
		}
		if (bClick)
		{
			guiStyle = getActiveGuiStyle(guiStyle);
		}

		// normal GUI button
		bool mouseClicked = GUI.Button(position, txt, guiStyle);
	
		bool niteCursorClicked = false;
		if (bClick && !NiteCursor.HandledLastClick)
		{
			niteCursorClicked = (ClickOnRelease && NiteCursor.IsClickRelease) || (!ClickOnRelease && NiteCursor.IsClickPush);
			NiteCursor.HandledLastClick = niteCursorClicked;
		}
		
		return mouseClicked || niteCursorClicked;
	}
	
	private static GUIStyle getActiveGuiStyle(GUIStyle guiStyle)
	{
		if (null == activeStyles)
		{
			activeStyles = new Dictionary<GUIStyle, GUIStyle>();
		}

		if (!activeStyles.ContainsKey(guiStyle))
		{
			GUIStyle activeStyle = new GUIStyle(guiStyle);
			activeStyle.normal = activeStyle.active;
			activeStyles[guiStyle] = activeStyle;
		}
		
		return activeStyles[guiStyle];
	}
	
	private static GUIStyle getHoverGuiStyle(GUIStyle guiStyle)
	{
		if (null == hoverStyles)
		{
			hoverStyles = new Dictionary<GUIStyle, GUIStyle>();
		}

		if (!hoverStyles.ContainsKey(guiStyle))
		{
			GUIStyle hoverStyle = new GUIStyle(guiStyle);
			hoverStyle.normal = hoverStyle.hover;
			hoverStyles[guiStyle] = hoverStyle;
		}
		
		return hoverStyles[guiStyle];
	}
	
	private static bool IsCursorOver(Rect position)
	{
		if (!NiteCursor.CursorActive)
		{
			return false;
		}
		
		return position.Contains(ToScreenCoords(NiteCursor.CursorPosition));
	}
	
	private static bool IsClickOver(Rect position)
	{
		if (!NiteCursor.CursorActive || !NiteCursor.IsClicked)
		{
			return false;
		}
		
		return position.Contains(ToScreenCoords(NiteCursor.ClickPosition));
	}
	
	private static Vector2 ToScreenCoords(Vector2 normalizedCoords)
	{
		return new Vector2(normalizedCoords.x * Screen.width, normalizedCoords.y * Screen.height);
	}
}
