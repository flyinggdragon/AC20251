# Interpolações:
O aplicativo possui duas cenas, com 4 pontos de controle e curvas interpoladas. As formas de interplação são: linear e curva BSpline. Também existe uma UI básica para navegação entre as duas cenas.

# Comandos (ou talvez só comando...):
ESC - Sair do aplicativo e voltar da cena para menu.

# Download e links:
- [Itch.io](https://flyingdrag0n.itch.io/ca20251interpolacoes)
- [Download pelo GitHub](https://github.com/flyinggdragon/AC20252/releases/tag/Interpolações)

# Estrutura e Funcionamento:
As duas interpolações estão representadas em duas classes separadas que herdam da classe Curve. Ela possui um método para gerar a curva com base nas posições dos pontos de controle. O método é marcado como ```virtual``` para que seja sobrescrito em suas classes filhas.
```cs
public class Curve : MonoBehaviour {
  public virtual void GenerateCurve(List<Transform> controlPoints) { }
}
```

```cs
public class LinearInterpolation : Curve {
  public override void GenerateCurve(List<Transform> controlPoints) {
    //...
  }
}
```

```cs
public class BSpline : Curve {
  public override void GenerateCurve(List<Transform> controlPoints) {
    //...
  }
}
```

A esfera que se move pelo caminho gerado possui a classe Ball associada. Ao carregar-se a cena, ela imediatamente chama o método de cálculo de curva da curva de interpolação selecionada, que por sua vez, gera pontos intermediários com base na curva gerada, e depois obtém-se suas posições no mundo.

Para as posições dos pontos de controle e intermediários, usa-se a estrutura ```Vector3```da Unity. Não foi necessário calcular o eixo Y. Coleta-se o componente ```Transform```, armazena-os em uma lista, e depois, quando necessário, acessam-se as posições por meio da propreidade position: ```controlPoints[i].position```.

```cs
public class Curve : MonoBehaviour {
  public void InstantiateIntermediatePoints(Vector3 point) {
    //...
  }
}
```

```cs
public class Ball : MonoBehaviour {

  //...
  public List<Transform> controlPoints;
  //...

  void Start() {
    curve = GetComponent<Curve>();
    curve.GenerateCurve(controlPoints);
  
    intermediatePoints = new List<Transform>(GetIntermediatePoints());
  }

  //...
}
```
