using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContentManager : MonoBehaviour
{
    [SerializeField] private UITables _uiTables;
    [SerializeField] private UIModels _uiModels;
    [SerializeField] private DescriptionPanel _descriptionPanel;
    [SerializeField] private Watcher _watcher;

    private void OnEnable()
    {
        _watcher.OnClickTable += HideUiTables;
        _watcher.OnClickBack += ShowUiTables;
        _watcher.OnClickBack += HideUiModels;
        _watcher.OnClickBack += HideDescriptionPanel;
        _watcher.OnClickContainer += ShowUiModels;
        _watcher.OnClickContainerWithout += HideDescriptionPanel;
        _watcher.OnClickModel += ShowDescriptionPanel;
    }

    private void OnDisable()
    {
        _watcher.OnClickTable -= HideUiTables;
        _watcher.OnClickBack -= ShowUiTables;
        _watcher.OnClickBack -= HideUiModels;
        _watcher.OnClickBack -= HideDescriptionPanel;
        _watcher.OnClickContainer -= ShowUiModels;
        _watcher.OnClickModel -= ShowDescriptionPanel;
    }

    private void HideUiTables()
    {
        _uiTables.Hide();
    }

    private void ShowUiTables()
    {
        _uiTables.Show();
    }

    private void ShowUiModels(IContainer container)
    {
        _uiModels.SetLookables(container.GetContents());
        _uiModels.CreateModelsButton();
        _uiModels.Show();
    }

    private void HideUiModels()
    {
        _uiModels.Hide();
    }

    private void ShowDescriptionPanel(IModel model)
    {
        _descriptionPanel.SetName(model.GetName());
        _descriptionPanel.SetDescription(model.GetDescription());
        _descriptionPanel.Show();
    }

    private void HideDescriptionPanel()
    {
        _descriptionPanel.Hide();
    }
}
