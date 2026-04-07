import { Box, Stack, Modal, Paper, Text, Flex, Badge, Menu, ActionIcon, UnstyledButton, Group, Button } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { useForm } from '@mantine/form';
import { IconStar, IconDots, IconPencil, IconTrash, IconPlus, IconCompass, IconActivity } from '@tabler/icons-react';
import PageTitle from '@/components/PageTitle';
import { useEffect, useState } from 'react';
import GoalCard from '@/components/goals/GoalCard';
import { theme } from '@/Theme';
import CreateNorthStarForm from '@/components/goals/CreateNorthStarForm';
import { useGetApiGoalGet } from '@/api/endpoints/goal/goal';

export default function Goals() {
  const [opened, { open, close }] = useDisclosure(false);
  const [activeForm, setActiveForm] = useState();
  const form = useForm({
    mode: 'uncontrolled',
    initialValues: {
      email: '',
      termsOfService: false,
    },

    validate: {
      email: (value) => (/^\S+@\S+$/.test(value) ? null : 'Invalid email'),
    },
  });

  const onGoalAdd = () => {
    setActiveForm(<CreateNorthStarForm />);
    open();
  }

  const GoalAddButton = ({ text }) => (
    <UnstyledButton w="100%" onClick={onGoalAdd}>
      <Group gap="md">
        <IconPlus size={12} />
        <Text size="xs" c="dimmed">{text}</Text>
      </Group>
    </UnstyledButton>
  )

  const { data: response, error, isLoading, mutate } = useGetApiGoalGet();

  return (
    <Stack gap="sm">
      <Group justify="space-between">
        <PageTitle name="Stars" description="Goals, represented as spots in the galaxy." />
        <Button leftSection={<IconPlus size={16} />} onClick={onGoalAdd}>New North Star</Button>
      </Group>

      <Modal opened={opened} onClose={close} title="Create Goal">
        {activeForm}
      </Modal>

      {response && response.data.map((star) => (
        <Stack>
          <Stack>
            <GoalCard
              name={star.name}
              type="star"
              description={star.description}
              left={<IconStar size={16} />}
              right={<Badge variant="light"
                color={theme.colors.priority[star.importance]}>{star.importance}</Badge>}
            />

            <Stack pl="lg" style={{ borderLeftWidth: "2px", borderLeftStyle: "solid", borderLeftColor: theme.colors.goal["star"] }}>
              {star.bearings.map((bearing) =>
              (
                <Stack gap="sm">
                  <GoalCard
                    name={bearing.name}
                    type="bearing"
                    description={bearing.description}
                    left={<IconCompass size={14} />}
                  />

                  <Stack gap="xs" pl="lg" style={{ borderLeftWidth: "2px", borderLeftStyle: "solid", borderLeftColor: theme.colors.goal["bearing"] }}>
                    {bearing.movements.map((movement) =>
                    (
                      <GoalCard
                        name={movement.name}
                        type="movement"
                        description={movement.description}
                        left={<IconActivity size={14} />}
                      />
                    ))}

                    <GoalAddButton text="Add Movement" />
                  </Stack>
                </Stack>
              ))}

              <GoalAddButton text="Add Bearing" />
            </Stack>
          </Stack>
        </Stack>
      ))}
    </Stack>
  );
}