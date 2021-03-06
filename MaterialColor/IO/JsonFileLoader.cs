﻿using Common.Data;
using Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MaterialColor.IO
{
    public class JsonFileLoader
    {
        public JsonFileLoader(JsonManager jsonManager, Common.IO.Logger logger = null)
        {
            _logger = logger;

            InitializeManagers(jsonManager);
        }

        private Common.IO.Logger _logger;

        private ConfiguratorStateManager _configuratorStateManager;
        private ElementColorInfosManager _elementColorInfosManager;
        private TypeColorOffsetsManager _typeColorOffsetsManager;

        private void InitializeManagers(JsonManager manager)
        {
            _configuratorStateManager = new ConfiguratorStateManager(manager, _logger);
            _elementColorInfosManager = new ElementColorInfosManager(manager, _logger);
            _typeColorOffsetsManager = new TypeColorOffsetsManager(manager, _logger);
        }

        public bool TryLoadConfiguratorState(out MaterialColorState state)
        {
            try
            {
                state = _configuratorStateManager.LoadMaterialColorState();
                return true;
            }
            catch (Exception ex)
            {
                var message = "Can't load configurator state.";

                _logger.Log(ex);
                _logger.Log(message);

                Debug.LogError(message);

                state = new MaterialColorState();

                return false;
            }
        }

        public bool TryLoadElementColorInfos(out Dictionary<SimHashes, ElementColorInfo> elementColorInfos)
        {
            try
            {
                elementColorInfos = _elementColorInfosManager.LoadElementColorInfosDirectory();
                return true;
            }
            catch (Exception e)
            {
                var message = "Can't load ElementColorInfos";

                Debug.LogError(message + '\n' + e.Message + '\n');

                State.Logger.Log(message);
                State.Logger.Log(e);

                elementColorInfos = new Dictionary<SimHashes, ElementColorInfo>();
                return false;
            }
        }

        public bool TryLoadTypeColorOffsets(out Dictionary<string, Color32> typeColorOffsets)
        {
            try
            {
                typeColorOffsets = _typeColorOffsetsManager.LoadTypeColorOffsetsDirectory();
                return true;
            }
            catch (Exception e)
            {
                var message = "Can't load TypeColorOffsets";

                Debug.LogError(message + '\n' + e.Message + '\n');

                State.Logger.Log(message);
                State.Logger.Log(e);

                typeColorOffsets = new Dictionary<string, Color32>();
                return false;
            }
        }
    }
}
