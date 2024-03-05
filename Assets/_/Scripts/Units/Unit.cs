using DG.Tweening;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerGame
{
    public enum ProductType
    {
        Soldier,
        Barrack,
        PowerPlant,
    }
    public enum CellColorState
    {
        CanMove,
        Normal,
        CantPut,
        Hit
    }

    public  class  Unit : MonoBehaviour 
    {        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] protected SpriteRenderer _tileRenderer;
        private List<Vector2> UnitCells=new List<Vector2>();
        protected ScriptableUnit _scriptableUnit;        
        protected GridManager _gridManager;        
        public CellStateType ProductType;
        private string _unitName;
        private int _width;
        private int _height;
        protected float _hp;
        
        bool _canPut;

        bool _builded;

        public List<Vector2> GetUnitCells =>UnitCells;
        public string GetName=> _unitName;

        
        public void SetBuildedState(bool state)=>_builded=state;
        public void Init(ScriptableUnit scriptableUnit)
        {
            _scriptableUnit = scriptableUnit;
            _unitName = scriptableUnit.GetName;
            _spriteRenderer.sprite = scriptableUnit.GetSprite;
            _width =  scriptableUnit.GetWidth;
            _height = scriptableUnit.Getheight;
            _hp = scriptableUnit.GetHp;
            ProductType = scriptableUnit.GetProductType;
            CreateProductCell(_width, _height);
           
        }
        private void OnEnable()
        {
            _gridManager = GridManager.Instance;
            GridEvents.UnitPositionRequest += GetUnitPositionRequest;
         
        }
        private void OnDisable()
        {
            GridEvents.UnitPositionRequest -= GetUnitPositionRequest;
          
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
        }

        //Drag Product
        void DragProduct()
        {
            MousePositionCalculate();
           CheckAvailable();
        }


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
            tempPosition.y = Mathf.Clamp(tempPosition.y, 0, _gridManager._scriptableGrid.GetGridheight - _height);
            transform.position = tempPosition;

        }

        private void GetUnitPositionRequest(Vector2 position)
        {
            List<Vector2> cellPositionList = CurrentCellPos();

            if (cellPositionList.Contains(position))
            {
                GridEvents.UnitPositionInfo?.Invoke(cellPositionList);

            }

        }
        //Build Product arae
        public void Build()
        {
            Vector3 tempPosition = transform.position;
            tempPosition.x = Mathf.Round(tempPosition.x);
            tempPosition.y = Mathf.Round(tempPosition.y);
            transform.position = tempPosition;
            GridEvents.BuildProductRequest?.Invoke(CurrentCellPos(), ProductType, this);
            _builded = true;
            ColorChange(CellColorState.Normal);
        }
        //Created Cells
        public void CreateProductCell(int rowSize, int columnSize)
        {
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
            _tileRenderer.transform.position = new Vector3(_tileRendererX, _tileRendererY, 0);
            _spriteRenderer.transform.position = new Vector3(_tileRendererX, _tileRendererY, 0);

           
             
        }
        // Get Current Cells Pos
        public List<Vector2> CurrentCellPos()
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
        //Check unit can be put area
        void CheckAvailable()
        {
            _canPut = true;
            List<Vector2> currentPositionList = CurrentCellPos();

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
        public IEnumerator UnitVisualHit()
        {
            ColorChange(CellColorState.Hit);
            yield return new WaitForSeconds(.1f);
            ColorChange(CellColorState.Normal);           
        }
        

    }
}