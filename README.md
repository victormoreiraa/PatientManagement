# Sistema de Gerenciamento de Pacientes

## Pré-requisitos

1- Visual Studio instalado. <br/>
2- .Net 7 instalado. <br/>
3- Vue.js instalado. <br/>

## Configurar e Executar o Projeto no Visual Studio

1- Abra o Visual Studio. <br/>
2- Selecione "Abrir Projeto/Solução" e navegue até o diretório raiz do projeto. <br/>
3- Selecione o arquivo de solução .sln e abra-o no Visual Studio. <br/>
4- Certifique-se de que tanto o projeto do backend (PatientManagement.API) quanto o projeto do frontend (patientmanagement.web) estão incluídos na solução. <br/>
5- Clique com botão direito na Solução que se encontra no Gerenciador de Soluções (Ctrl + ç) e selecione "Configurar projetos de inicialização", após isso selecione a opção "Vários projetos de inicialização" e coloque os projetos PatientManagement.API e o patientmanagement.web para iniciarem juntos. <br/>
7- Certifique-se de que a url ( exemplo: "http://localhost:5111/api/patient/") da API é a mesma usada nas requisiçôes no projeto patientmanagement.web, elas serão encontradas na pasta components dentro do arquivo Index.vue. <br/>

## Funcionalidades

- CRUD de Pacientes <br/>
- Validação de Resultados dos Exames <br/>


## Como usar o Sistema

- O Sistema abrirá na pagina de gerenciamento onde se encontra a tabela com todos os pacientes cadastrados e seus  resultados de exame. <br/>
- Para adicionar um paciente clique no botão de novo paciente e adicione os dados solicitados (todos os campos são obrigatórios), após salvar ele aparecerá na tabela com o resultado de seus exames baseado nos dados informados. <br/>
- Cada dado da tabela possui duas ações, a de excluir e a de editar os dados de paciente que ao ser clicada vai abrir um modal com todos os dados em seus respectivos campos. As duas funçoes estão em botões localizados no lado direto da tabela. <br/>
