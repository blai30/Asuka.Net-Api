CREATE TABLE asuka_net.tags
(
    id          SERIAL  NOT NULL PRIMARY KEY,
    name        VARCHAR NOT NULL,
    content     VARCHAR NOT NULL,
    reaction    VARCHAR,
    guild_id    BIGINT  NOT NULL,
    user_id     BIGINT  NOT NULL,
    usage_count INT     NOT NULL DEFAULT 0,
    created_at  TIMESTAMP        DEFAULT CURRENT_TIMESTAMP,
/* There cannot be more than one tag with the same name in the same guild. */
    CONSTRAINT unique_per_guild UNIQUE (name, guild_id)
);

CREATE TABLE asuka_net.reaction_roles
(
    id         SERIAL  NOT NULL PRIMARY KEY,
    guild_id   BIGINT  NOT NULL,
    channel_id BIGINT  NOT NULL,
    message_id BIGINT  NOT NULL,
    role_id    BIGINT  NOT NULL,
    reaction   VARCHAR NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
