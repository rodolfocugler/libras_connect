#LIBRAS CONNECT

##RECONHECIMENTO DE LIBRAS COM CNTK E REALSENSE

Este projeto foi desenvolvido como TCC do curso de engenharia da computação dos alunos Felipe da Silva Pereira e Rodolfo Luiz Cugler Pereira no ano de 2017 na Faculdade de Engenharia de Sorocaba.

A monografia final pode ser encontrada em Documents/monografia.pdf

A ideia do projeto é fazer o reconhecimento de linguagem brasileira de sinais (LIBRAS) utilizando duas câmeras Intel Realsense F200 https://www.intel.com/content/www/us/en/support/products/92255/emerging-technologies/intel-realsense-technology/intel-realsense-cameras/intel-realsense-camera-f200.html e o toolkit de deep learning da Microsoft CNTK.

A documentação da câmera da Intel pode ser encontada em https://software.intel.com/sites/landingpage/realsense/camera-sdk/v2016r3/documentation/html/index.html?doc_hand_accessing_hand_posture_and_sid.html

##Para rodar o projeto
É possível encontrar os dados capturados por nós na pasta Documents/banco de dados/ e é necessário importar esses dados para um banco de dados MongoDB.

mongoimport --db libras_connect --collection Signal --file Signal.json
mongoimport --db libras_connect --collection WordDNN --file WordDNN.json

As aplicações foram construídas em C# e Python 3.6 (código do rna)

## Aplicação C#
As aplicações em C# devem ser compiladas em 64bits
A solution contém 3 projetos:

### libras-connect-camera
Responsável por pegar informação da câmera e enviar para o servidor

Na execução os parametros devem ser informados

Opção de execução: 
1 - Envia os dados das mãos como coordenadas 
2 - Envia a imagem RGB
3 - Aplicação para fazer testes sem a câmera (versão beta)

IP do servidor:
IP:porta

### libras-connect-client
Responsável por receber as informação dos clientes
No menu -> setting deve ser configurado o IP do servidor (IP da máquina local) e as portas que os sockets serão abertos

Em \libras-connect-infrastructure\Config existe um arquivo onde as seguintes configurações devem ser feitas:

Solution_Path = @"C:\Users\rodol\Documents\Projetos\tcc"; //Caminho da solução
Dnn_Path = Solution_Path + @"\libras_connect.dnn"; //Caminho onde se encontra o arquivo .dnn gerado pelo cntk
Mongo_IP = "127.0.0.1"; //IP do banco MongoDB
Mongo_Database = "libras_connect"; //Nome do banco no mongo
Cntk_Threshold = 0.7; //Limite mínimo para o cntk interpretar uma resposta como verdade

Para salvar os dados deve se atributer uma label(palavra) no input da página principal e clicar em salvar para começar a salvar os quadros e depois em parar para parar de salvá-los

Quando o software não está salvando quadro ele está analisando-os

### libras-connect-test
IP do banco de dados MongoDB no padrão IP:porta/nome_do_banco

É possível gerar os arquivos que serão utilizados pelo software em Python para criar a rede neural com os dados armazenados no MongoDB

Em \libras-connect-infrastructure\Config existe um arquivo onde as seguintes configurações devem ser feitas:

Train_Txt = Solution_Path + @"\python\data\train.txt"; //Arquivo que será gerado para treinar a rede neural
Test_Txt = Solution_Path + @"\python\data\test.txt"; //Arquivo que será gerado para testar a rede neural

É possível testar a rede neural com os dados armazenados no MongoDB

## Aplicação Python 

Responsável por criar a rede neural 

Em \python\libras_connect existe um arquivo onde as seguintes configurações devem ser feitas:

MAX_EPOCHS = 300 // máximo de épocas de treinamento
NUM_LABELS = 13 // quantidade de palavras treinadas 

FILEPATH = "C:\\users\\rodol\\Documents\\Projetos\\tcc\\python" //caminho onde será salvo o arquivo .dnn

Executar com: "python /main.py"










