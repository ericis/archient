namespace Archient.VisualStudio.Guidance.Templates.Wizards

open Microsoft.VisualStudio.TemplateWizard

type BlankSolutionWizard() =
    interface IWizard with
        
        override me.BeforeOpeningFile(projectItem) = 
            ()

        override me.ProjectFinishedGenerating(project) = 
            ()

        override me.ProjectItemFinishedGenerating(projectItem) = 
            ()

        override me.RunFinished() = 
            ()

        override me.RunStarted(automationObject, replacementsDictionary, runKind, customParams) = 
            ()

        override me.ShouldAddProjectItem(filePath) = 
            false