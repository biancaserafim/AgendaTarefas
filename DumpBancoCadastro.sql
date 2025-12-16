-- 1. Criar o Banco de Dados
CREATE DATABASE IF NOT EXISTS `sistema_agenda`;
USE `sistema_agenda`;

-- 2. Criar a Tabela (conforme sua imagem do Workbench)
DROP TABLE IF EXISTS `tarefas`;
CREATE TABLE `tarefas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `titulo` varchar(255) NOT NULL,
  `data_tarefa` datetime DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- 3. Procedure: Listar
DELIMITER ;;
CREATE PROCEDURE `sp_ListaTarefas`()
BEGIN
    SELECT id, titulo, data_tarefa, status FROM tarefas ORDER BY data_tarefa;
END ;;
DELIMITER ;

-- 4. Procedure: Inserir
DELIMITER ;;
CREATE PROCEDURE `sp_InsereTarefa`(
    p_descricao varchar(255), 
    p_data datetime, 
    p_concluida varchar(50)
)
BEGIN
    INSERT INTO tarefas (titulo, data_tarefa, status) 
    VALUES (p_descricao, p_data, p_concluida);
END ;;
DELIMITER ;

-- 5. Procedure: Alterar
DELIMITER ;;
CREATE PROCEDURE `sp_AlteraTarefa`(
    p_id int,
    p_descricao varchar(255), 
    p_data datetime, 
    p_concluida varchar(50)
)
BEGIN
    UPDATE tarefas 
    SET titulo = p_descricao, data_tarefa = p_data, status = p_concluida 
    WHERE id = p_id;
END ;;
DELIMITER ;

-- 6. Procedure: Remover
DELIMITER ;;
CREATE PROCEDURE `sp_RemoveTarefa`(p_id int)
BEGIN
    DELETE FROM tarefas WHERE id = p_id;
END ;;
DELIMITER ;