using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class BlockNote {

    public string notes;

    public BlockNote() {}

    public void Reinit()
    {
        notes = "";
    }

    public void AddNote()
    {
        //Question
        string question = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.DialogueText;
        //Reponse
        int idConv = DialogueManager.ConversationModel.GetConversationID(DialogueManager.CurrentConversationState);
        int idLink = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.outgoingLinks[0].destinationDialogueID;
        DialogueEntry entry = DialogueManager.Instance.DatabaseManager.MasterDatabase.GetDialogueEntry(idConv, idLink);
        string reponse = entry.SubtitleText;
        notes += question + "\n\t- " + reponse + "\n";
    }

    public void AddNote2()
    {
        notes = "\n-Présence de plaques rouges après la douche\n\n- Démangeaisons\n\n- Dernier repas il y à 21 heures";
    }
}
