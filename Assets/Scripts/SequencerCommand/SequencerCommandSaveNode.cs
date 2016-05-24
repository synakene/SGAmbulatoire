using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandSaveNode : SequencerCommand {

		public void Start() {

            int level = int.Parse(GetParameter(0));

            int idConv = DialogueManager.ConversationModel.GetConversationID(DialogueManager.CurrentConversationState);
            string question = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.DialogueText;
            string goodAnswer = "";
            int idGoodAnswer = 0;

            List<Link> outgoingLinks = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.outgoingLinks;
            Link firstChild = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.outgoingLinks[0];
            DialogueEntry LinkedEntry = DialogueManager.Instance.DatabaseManager.MasterDatabase.GetDialogueEntry(idConv, firstChild.destinationDialogueID);

            for (int i = 0; i < level; i++)
            {
                LinkedEntry = DialogueManager.Instance.DatabaseManager.MasterDatabase.GetDialogueEntry(idConv, firstChild.destinationDialogueID);
                firstChild = LinkedEntry.outgoingLinks[0];
                outgoingLinks = LinkedEntry.outgoingLinks;
            }

            if (Data.allFeedback == null)
            {
                Data.CompleteFeedback = new CompleteFeedback();
                Parser.ParseFeedback();
            }

            //Get idGoodAnswer
            foreach (Feedback f in Data.allFeedback)
            {
                foreach (Link link in outgoingLinks)
                {
                    int idLink = link.destinationDialogueID;
                    if (idConv == f.IdConv && idLink == f.IdNode && f.IdNode == f.IdGoodAnswer)
                    {
                        idGoodAnswer = f.IdGoodAnswer;
                        break;
                    }
                }
            }

            CompleteFeedback.Info info = new CompleteFeedback.Info(question);

            foreach (Link link in outgoingLinks)
            {
                int idLink = link.destinationDialogueID;
                DialogueEntry entry = DialogueManager.Instance.DatabaseManager.MasterDatabase.GetDialogueEntry(idConv, idLink);
                string reponse = DialogueManager.ConversationModel.GetState(entry).subtitle.dialogueEntry.DialogueText;
                if (idGoodAnswer == idLink)
                {
                    goodAnswer = reponse;
                }
                info.AddChoice(idLink, reponse);
            }

            info.AddGoodAnswer(idGoodAnswer, goodAnswer);

            Data.CompleteFeedback.AddNode(info);

            Stop();
        }
	}
}
