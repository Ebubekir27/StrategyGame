using DG.Tweening;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UIElements;

namespace StrategyGame
{
    public  class  Unit : MonoBehaviour 
    {        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] protected SpriteRenderer _tileRenderer;
        private List<Vector2> UnitCells=new List<Vector2>();
        protected ScriptableUnit _scriptableUnit;        
        protected GridManager _gridManager;        
        public CellStateType CellStateType;
        private string _unitName;
        private int _width;
        private int _height;        
        protected float _baseHp;
        protected float _currentHp;        
        protected bool _attackable;
        private bool _canPut;
        private bool _builded;

        public List<Vector2> GetUnitCells =>UnitCells;
        public string GetName=> _unitName;
        public float GetCurrentHp => _currentHp;
        public float GetBaseHp => _baseHp;
        public bool GetAttackable =>  _attackable;
        public int GetUnitWidth => _width;
        public int GetUnitHeight => _height;

        public ScriptableUnit GetScriptableUnit => _scriptableUnit;


        
        public void SetBuildedState(bool state)=>_builded=state;
        public void Init(ScriptableUnit scriptableUnit)
        {
            _scriptableUnit = scriptableUnit;
            _unitName = scriptableUnit.GetName;
            _spriteRenderer.sprite = scriptableUnit.GetSprite;
            _width =  scriptableUnit.GetWidth;
            _height = scriptableUnit.Getheight;
            _baseHp= _currentHp = scriptableUnit.GetHp;            
            CellStateType = scriptableUnit.GetProductType;
            CreateProductCell(_width, _height);
           
        }
        private void OnEnable()
        {
            _gridManager = GridManager.Instance;                  
        }
              
        private void Update()
        {
            if ( !_builded)
            {
                DragProduct();                           
            }
            if (Input.GetMouseButtonDown(0) && _canPut && !_builded)
            {
                Build();
               
            }
            if (Input.GetMouseButtonDown(1) && !_builded)
            {
                Destroy(gameObject);
            }
        }

        //Drag Product
        void DragProduct()
        {
            MousePositionCalculate();
           CheckAvailable();
        }

        // Mouse Position calculate for unit transform center
        void MousePositionCalculate()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = -5;
            mousePosition.x -= (_width - 1) * .5f;
            mousePosition.y -= (_height - 1) * .5f;
            Vector3 tempPosition = mousePosition;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = Mathf.Round(tempPosition.y);
            tempPosition.x = Mathf.Clamp(tempPosition.x, 0, _gridManager._scriptableGrid.GetGridWidth - _width);
            tempPosition.y = Mathf.Clamp(tempPosition.y, 0, _gridManager._scriptableGrid.GetGridHeight - _height);
            transform.position = tempPosition;

        }

        //Build Product arae
        public void Build()
        {
            Vector3 tempPosition = transform.position;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = Mathf.Round(tempPosition.y);
            transform.position = tempPosition;
            GridEvents.BuildProductRequest?.Invoke(CurrentCellsPos(), CellStateType, this);
            _builded = true;
            ColorChange(CellColorState.Normal);
        }
        //Created Cells
        public void CreateProductCell(int rowSize, int columnSize)
        {
            UnitCells.Clear();
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    UnitCells.Add(new Vector2(i, j));
                
                }
            }
            
            _tileRenderer.size = new Vector2(rowSize, columnSize);
            _spriteRenderer.size = new Vector2(rowSize, columnSize);
            float _tileRendererX = Mathf.Clamp((float)(rowSize - 1) / 2, 0, rowSize);
            float _tileRendererY= Mathf.Clamp((float)(columnSize - 1) / 2,0, columnSize);

            _tileRenderer.transform.localPosition = new Vector3(_tileRendererX, _tileRendererY, 0);
            _spriteRenderer.transform.localPosition = new Vector3(_tileRendererX, _tileRendererY, 0);

           
             
        }
        // Get Current Cells Pos
        public List<Vector2> CurrentCellsPos()
        {
            List<Vector2> currentPositionList = new List<Vector2>();
            Vector3 tempPosition = transform.position;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = Mathf.Round(tempPosition.y);           
            for (int i = 0; i < UnitCells.Count; i++)
            {
                currentPositionList.Add(new Vector2(tempPosition.x+ UnitCells[i].x, tempPosition.y + UnitCells[i].y));
            }

            return currentPositionList;
        }
        public Vector2 CurrentCellPos()
        {
            Vector3 tempPosition = transform.position;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = Mathf.Round(tempPosition.y);
            

            return tempPosition;
        }
        //Check unit can be put area
        void CheckAvailable()
        {
            _canPut = true;
            List<Vector2> currentPositionList = CurrentCellsPos();

            for (int i = 0; i < currentPositionList.Count; i++)
            {
                 
                if (CheckCell(currentPositionList[i]) != CellStateType.Empty)
                {
                    _canPut = false; break;
                }
            }
            //Visual
            if (_canPut)
            {
                ColorChange(CellColorState.Normal);

            }
            else
            {
                ColorChange(CellColorState.CantPut);

            }

        }
       
        //Check cell for empty
        CellStateType CheckCell(Vector2 cellPosition)
        {
            CellStateType cellType = CellStateType.Empty;
            Node node;
            _gridManager.Cells.TryGetValue(cellPosition, out node);
            cellType = node.CellState;

            return cellType;
        }
        
        //Visual
        public void ColorChange(CellColorState colorState)
        {
            Color color = Color.white;
            if (colorState == CellColorState.Normal)
            {
                color = Color.green;
            }
            else if (colorState == CellColorState.CantPut|| colorState == CellColorState.Hit)
            {
                color = Color.red;
            }
            else if (colorState == CellColorState.CanMove)
            {
                color = Color.blue;
            }
            
            _tileRenderer.color = color;
            
        }

        // Color changes when damaged
        public IEnumerator UnitVisualHit()
        {
            ColorChange(CellColorState.Hit);
            yield return new WaitForSeconds(.1f);
            ColorChange(CellColorState.Normal);           
        }
        

    }
}