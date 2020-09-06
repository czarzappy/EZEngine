﻿ namespace ZEngine.Core.Dialogue.Models
{
    public class Episode
    {
    }

    public class Dialogue
    {
        public string CharacterDevName;
        public string Text;
    }

    public struct CharacterMove
    {
        public string CharacterDevName;
        
        // 1-indexed
        public int NewStagePos;

        public CharacterMove(string characterDevName, int newStagePos)
        {
            this.CharacterDevName = characterDevName;
            this.NewStagePos = newStagePos;
        }

        public override string ToString()
        {
            return $"{CharacterDevName} -> {NewStagePos}";
        }
    }
}
