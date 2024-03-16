using System;
using UnityEngine;

namespace Sticks
{
    public class BreakableStick : CommonStick
    {
        public bool canBeHanging => _restoreDelay <= 0;

        [SerializeField]
        float _initialFallDelay;
        [SerializeField]
        float _initialRestoreDelay;
        [SerializeField]
        GameObject _wholeStick;
        [SerializeField]
        GameObject _damagedStick;

        float _fallDelay;
        float _restoreDelay;

        Guid _id = Guid.NewGuid();
        CatController _catController;
        bool _dirtyFall;
        bool _dirtyRestore;

        void Start()
        {
            _catController = FindObjectOfType<CatController>();
            _wholeStick.SetActive(true);
            _damagedStick.SetActive(false);
        }

        public Guid SetHanging()
        {
            _id = Guid.NewGuid();

            _fallDelay = _initialFallDelay;
            _dirtyFall = true;
            _dirtyRestore = true;

            return _id;
        }

        void Update()
        {
            if (_fallDelay > 0)
            {
                _fallDelay -= Time.unscaledDeltaTime;
                return;
            }

            if (_dirtyFall)
            {
                _dirtyFall = false;
                _restoreDelay = _initialRestoreDelay;
                _catController.RemoveFromStick(_id);
                _id = Guid.NewGuid();
                _wholeStick.SetActive(false);
                _damagedStick.SetActive(true);
            }

            if (_restoreDelay > 0)
            {
                _restoreDelay -= Time.unscaledDeltaTime;
                return;
            }

            if (_dirtyRestore)
            {
                _dirtyRestore = false;
                _wholeStick.SetActive(true);
                _damagedStick.SetActive(false);
            }
        }
    }
}
