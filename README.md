# Sistema para level design

![](http://i.giphy.com/26FxCd1xmbA8z6OIg.gif)

Exemplo de integração de diversas ferramentas para level design, com o objetivo de servir de base para exercícios de criação de fases, progressão e balanceamento de jogos. Criado para a disciplina de Balanceamento de Jogos, na PUC-Campinas, 1º semestre de 2016.

## Ferramentas

### [Tiled](http://www.mapeditor.org/)

Editor de mapas que permite a criação de mapas do tipo *tilemap* baseados em *tiles* ou blocos reutilizáveis, organizados em camadas e baseados em imagens com vários blocos (chamadas de *tilesheets* ou *tilesets*). O editor também permite a organização de objetos e áreas úteis (triggers). Tanto tiles quanto objetos podem receber todo tipo de propriedade, que depois são tratadas e utilizadas dentro do jogo.

### [Tiled2Unity](http://www.seanba.com/tiled2unity)

Programa dedicado a realizar a transição entre mapas criados no Tiled e a Unity. Basicamente, ele cria um prefab com diferentes objetos representando as camadas criadas no Tiled e já cuida também da importação e visualização das imagens do mapa. As camadas de tiles são importadas com os gráficos, mas os objetos são importados apenas com seu nome. A partir deles, pode-se criar um script que os utiliza como referência para instanciar os prefabs desejados.

A escala dos mapas (*vertex scale*) é calculada a partir da densidade de um tile na imagem (1 / Pixels por metro).

#### Propriedades

- `unity:layer`: coloca os objetos dessa camada do Tiled em uma `Layer` da Unity.
- `unity:isTrigger`: altera a propriedade `isTrigger` dos colliders de objetos dessa camada.
- `unity:sortingLayerName`: objetos são colocados em uma `SortingLayer` para ordenação visual.

#### `PrefabSpawner` e `PrefabSpawnerEditor`

Estes dois scripts procuram a cena atual por objetos com a tag `Spawner`, para depois comparar com os prefabs na pasta `Resources`. Se encontrar os mesmos nomes, ele substitui esses objetos temporários pelos prefabs finais. É prático para facilitar a edição no Tiled, onde você pode ver todo o layout da fase sem ter muito retrabalho na hora de substituir pelos prefabs.

## Elementos de level design

### Tiles

Tiles são blocos que podem ter diversos usos na criação de espaços de jogo (*gamespaces*): desde decoração e ambientação até o volume e contornos do espaço. Para organizar o seu uso, é comum separá-los em camadas com solidez e ordens de desenho diferentes.

### Blocos e cristais

Essa mecânica é baseada na ideia de mudar os volumes e mobilidade dos espaços passados. Quando você coleta um cristal de uma determinada cor, você ativa os blocos daquela cor. Isso é feito criando duas camadas separadas, uma para cada cor de bloco, e colocando os tiles lá. Depois, dentro da engine, um script (`BlockManager`), liga e desliga essas camadas quando jogador toca nos cristais.

### Portas verdes e vermelhas

As portas verdes tem a função de transportar o jogador para um outro ponto do cenário. Com isso, o cenário ganha ritmo e a sensação de espaço pode ser aumentada. Tecnicamente, quando o jogador pega uma chave, ele abre a porta que está marcada na chave (através de uma variável pública no `Inspector`). 

As portas vermelhas utilizam o mesmo mecanismo de chaves para serem abertas, mas quando elas são atravessadas, o jogador carrega uma nova fase (seguindo a ordem de cenas que foram adicionadas aos `BuildSettings`).

### Escadas

A escada permite deslocamentos verticais mais controlados e com um ritmo obrigatoriamente mais lento. Tecnicamente, são uma camada separada com um trigger, que desliga a gravidade quando o personagem está sobre ela e ativa o controle de escalada (botão para cima) no script do `Player`.

### Espinhos e `DeathZone`

São áreas do jogo que "matam" o jogador e o enviam de volta para o início da fase. Basicamente, a fase é recarregada em seu estado inicial. São uma forma de permitir ao jogador corrigir seus erros e também de puni-los, criando tensão e exigindo novas manobras e táticas.

### Placas com texto

Uma placa detecta quando o jogador está sobre ela (`OnTriggerEnter2D`) e abre uma caixa de diálogo preparada anteriormente, muda o texto para sua mensagem e depois a fecha quando o jogador sai da placa. A ideia é que sejam usadas para comunicar verbalmente com os jogadores.

### Plataformas móveis

Esse elemento é bem recorrente, pois é muito flexível. Permite a criação de cenas de tensão através de trajetórias e uma remoção do controle do jogador. Além disso, tem uma relação forte com ritmo e tempo. Tecnicamente, os caminhos da trajetória de uma plataforma são criado no Tiled, usando a ferramenta de *polylines*. Depois, dentro da Unity, a ferramenta Tiled2Unity converte isso para um `EdgeCollider2D`, que guarda uma lista de pontos. O script `MovingPlatform` usa essa lista de pontos para criar um tween (uma animação) controlando o movimento do jogador. Para fazer o personagem acompanhar a plataforma, ele se torna um "filho" da plataforma enquanto está tocando nela.

---
**Enric Llagostera** | [email](mailto:enricllagostera@gmail.com) | [facebook](http://www.facebook.com/enricllagostera)

Todo o conteúdo original deste repositório e sua wiki está sob a licença [CC-BY-NC-SA](https://creativecommons.org/licenses/by-nc-sa/4.0/), a não ser quando marcado de outra forma. 

[![](https://licensebuttons.net/l/by-nc-sa/3.0/88x31.png)](https://creativecommons.org/licenses/by-nc-sa/4.0/)
